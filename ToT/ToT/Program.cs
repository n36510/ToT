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
        static Login login;
        static WorldSelect world;
        static RenderWindow win;
        static bool isWorldSelect = false;
        static bool isLogin = true;
        static Chat chat;
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
                                byte status = b.ReadByte();

                                if(status == 1)
                                {
                                    login.win.visible = false;
                                    world = new WorldSelect(win, client);
                                    isLogin = false;
                                    isWorldSelect = true;
                                    byte[] pack = new byte[1024];
                                    MessageBuffer p = new MessageBuffer(pack);
                                    p.WriteInt16((short)0x0004);
                                    p.WriteByte(0x00);
                                    client.Client.Send(pack);
                                } else
                                {
                                    Console.WriteLine("Invalid Password...");
                                }

                                break;
                            }
                        case (short)recvOps.selectWorld:
                            {
                                break;
                            }
                        case (short)recvOps.worldList:
                            {
                                byte id = b.ReadByte();
                                Sprite sprite = new Sprite(new Texture("Sprites/worlds/"+id+".png"));
                                world.worlds.Add(sprite);
                                break;
                            }
                        case (short)0x0005:
                            {
                               
                                chat = new Chat(win, client);
                                isWorldSelect = false;
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
                win = new RenderWindow(new SFML.Window.VideoMode(800, 600, 32), "ToT");
                // Setup event handlers
                win.Closed += new EventHandler(OnClosed);
                login = new Login(win, client);
                while (win.IsOpen)
                {
                    win.DispatchEvents();
                    if (isWorldSelect)
                    {
                        world.Update();
                    }
                    win.Clear();
                    if (isLogin)
                    {
                        login.Draw();
                    } else if (isWorldSelect)
                    {
                        world.Draw();
                    }
                    if (!isLogin && !isWorldSelect)
                    {
                        chat.Draw();
                    }

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
