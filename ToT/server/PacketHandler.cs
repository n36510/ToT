﻿/*
 * Copyright 2018 James D Pennington Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using MySql.Data.MySqlClient;
using System.Net.Sockets;
using System.Text;
using System.Security.Cryptography;

namespace server
{
    class PacketHandler
    {
        public MySqlConnection mysql;
        public PacketHandler()
        {
            mysql = new MySqlConnection("server=localhost;user=root;database=tot;port=3306;password=sn0poqmz");
            
        }

        public void doLogin(TcpClient client, string userName, string passWord)
        {
            Console.WriteLine("Login Info Recieved");
            
            mysql.Open();
            string sql = "SELECT * FROM user WHERE user='"+userName+"'";
            MySqlCommand cmd = new MySqlCommand(sql, mysql);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    string user = (string)rdr["user"];
                    string pass = (string)rdr["pass"];
                    //Console.WriteLine("DB: " + pass + " Input: " + passWord);
                   
                    if (pass == passWord)
                    {
                        Console.WriteLine("User " + user + " logged on from " + client.Client.RemoteEndPoint);
                        byte[] p = new byte[1024];
                        MessageBuffer packet = new MessageBuffer();
                        packet.WriteShort((short)0x0001);
                        packet.WriteByte(1);
                        p = packet.GetContent();
                        client.Client.Send(p);
                       
                        Program.clients.Add(new Session(client, user));

                        //accountName = user;
                    } else
                    {
                        Console.WriteLine("Invalid Password for account " + user + " from " + client.Client.RemoteEndPoint);
                        byte[] p = new byte[1024];
                        MessageBuffer packet = new MessageBuffer();
                        packet.WriteShort((short)0x0001);
                        packet.WriteByte(2);
                        p = packet.GetContent();
                        client.Client.Send(p);
                    }
                }
                rdr.Close();
            } else
            {
                rdr.Close();
                Console.WriteLine("Account created: "+ userName);
                string ins = "INSERT INTO user (`user`, `pass`) VALUES ('"+userName+"','"+passWord+"')";
                MySqlCommand insert = new MySqlCommand(ins, mysql);
                insert.ExecuteReader();
                byte[] p = new byte[1024];
                MessageBuffer packet = new MessageBuffer();
                packet.WriteShort((short)0x0001);
                packet.WriteByte(1);
                p = packet.GetContent();
                client.Client.Send(p);
                Program.clients.Add(new Session(client, userName));
                // accountName = userName;
            }
            
            mysql.Close();
        }
        public void sendWorlds(TcpClient c)
        {
            byte[] response = new byte[1024];
            MessageBuffer b = new MessageBuffer();
            b.WriteShort((short)0x0004);
            b.WriteByte(0);
            response = b.GetContent();
            c.Client.Send(response);
        }
    }
}
