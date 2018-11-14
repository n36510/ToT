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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.Net.Sockets;
namespace ToT
{
    class Login
    {
        UIWindow win;
        UITextBox username;
        UITextBox password;
        UIButton btn;
        RenderWindow rw;
        TcpClient client;
        public Login(RenderWindow w, TcpClient c)
        {
            client = c;
            win = new UIWindow(w, 300, 200, 250, 350, "Login");
            username = new UITextBox(w, win, 10, 50, 250, false);
            password = new UITextBox(w, win, 10, 100, 250, true);
            btn = new UIButton(w, win, 10, 150, 100, 25, doLogin, "Log In");
        }
        void doLogin()
        {
            byte[] buffer = new byte[1024];
            MessageBuffer b = new MessageBuffer(buffer);
            b.WriteInt16((short)0x0001);
            b.WriteString(username.text);
            b.WriteString(password.text);
            client.Client.Send(buffer);
        }
        public void Draw()
        {
            
            win.Draw();
            username.Draw();
            password.Draw();
            btn.Draw();
        }
    }
}
