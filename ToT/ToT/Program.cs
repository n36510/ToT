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
using System.Net.Sockets;
using SFML.Graphics;
using System.Threading;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using KeyEventArgs = SFML.Window.KeyEventArgs;

namespace ToT
{
    class Program
    {
        static TcpClient client;
        static void threadMain()
        {
            byte[] data = new byte[1024];
            for (; ; )
            {
                try {
                    client.Client.Receive(data);
                    MessageBuffer b = new MessageBuffer(data);
                    short opcode = b.ReadInt16();
                    switch (opcode)
                    {
                        case (short)recvOps.login:
                            {
                                break;
                            }
                        case (short)recvOps.selectWorld:
                            {
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    }
                }
                catch (Exception e)
                {
                    break;
                }
            } 
        }
        static void Main(string[] args)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8484);

                Thread t = new Thread(new ThreadStart(() => { threadMain(); }));
                t.Start();
                RenderWindow win = new RenderWindow(new SFML.Window.VideoMode(800, 600, 32), "ToT");
                // Setup event handlers
                win.Closed += new EventHandler(OnClosed);
                Login login = new Login(win);
                while (win.IsOpen)
                {
                    win.DispatchEvents();
                    
                    win.Clear();
                    login.Draw();
                    win.Display();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An Error Occured: " + e.Message, "ToT");
                return;
            }
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            RenderWindow win = (RenderWindow)sender;
            win.Close();
        }
    }
}
