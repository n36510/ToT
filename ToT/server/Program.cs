/*
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
using System.Threading;
using System.Net.Sockets;

namespace server
{
    class Program
    {
        static void mainThread(TcpClient c)
        {
            // TODO: Create a ByteBuffer Class;

            byte[] data = new byte[1024];
            Console.WriteLine("Client connected from: " + c.Client.RemoteEndPoint);
            for(; ;)
            {
                try
                {
                    c.Client.Receive(data);
                    MessageBuffer b = new MessageBuffer(data);
                    short opcode = b.ReadInt16();

                    switch(opcode)
                    {
                        default:
                            {
                                Console.WriteLine("Unknown Packet: " + opcode);
                                break;
                            }
                    }

                } catch (Exception e)
                {

                    Console.WriteLine("Client Disconnected.");
                    return;

                }
                // Handle Packets

            }
        }
        static void Main(string[] args)
        {
            // Setup a TCP Server on port 8484.
            TcpListener l = new TcpListener(8484);
            l.Start();
            Console.WriteLine("Server is up and listening on port 8484");

            // Main Loop;
            for(; ;)
            {
                // Accept the client.
                TcpClient client = l.AcceptTcpClient();

                // Form a thread for each new client.
                Thread t = new Thread(new ThreadStart(() => {
                    mainThread(client);
                }));
                t.Start();

            }
        }
    }
}
