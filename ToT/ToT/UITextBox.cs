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
using SFML.Window;

namespace ToT
{
    class UITextBox
    {

        private Text displayText;
        public string text;
        private string pText;
        public bool isFocused = false;
        bool password;
        int x, y;
        RectangleShape tBox;
        int w;
        UIWindow ui;

        RenderWindow window;
        public UITextBox(RenderWindow win, UIWindow uiw, int xx, int yy, int width, bool pass)
        {
            ui = uiw;

            this.password = pass;
            window = win;
            this.w = width;
            this.displayText = new Text();
            this.displayText.Font = new Font("Tahoma.ttf");
            this.displayText.Color = Color.Black;
            this.displayText.Position = new SFML.System.Vector2f(uiw.winX + xx, uiw.winY + yy);
            this.displayText.CharacterSize = 12;
            this.tBox = new RectangleShape(new SFML.System.Vector2f(width, 25));
            this.tBox.FillColor = Color.White;
            this.tBox.Position = new SFML.System.Vector2f(uiw.winX + xx, uiw.winY +yy);
            win.TextEntered += new EventHandler<TextEventArgs>(KeyboardInput);
            win.KeyPressed += new EventHandler<KeyEventArgs>(bsp);
            win.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(MouseInput);
            this.x = uiw.winX + xx;
            this.y = uiw.winY + yy;
        }
        private void bsp(object sender, KeyEventArgs args)
        {
            if (isFocused) {
                if (args.Code == Keyboard.Key.BackSpace)
                {
                    

                }
            }
        }
        private void MouseInput(object sender, MouseButtonEventArgs args)
        {

            FloatRect pos = new FloatRect(this.x, this.y, this.tBox.GetGlobalBounds().Width, this.tBox.GetGlobalBounds().Height);
            if (pos.Contains(args.X, args.Y))
            {
                this.isFocused = true;
            } else
            {
                this.isFocused = false;
            }

        }

        private void KeyboardInput(object sender, TextEventArgs args)
        {
            if (this.isFocused)
            {
                if (args.Unicode == "\b")
                {
                    if (!String.IsNullOrEmpty(this.pText) && !String.IsNullOrEmpty(this.text))
                    {
                        this.pText = this.pText.Remove(this.pText.Length-1);
                        this.text = this.text.Remove(this.text.Length-1);
                        if (!password)
                        {
                            this.displayText.DisplayedString = this.text;
                        } else
                        {
                            this.displayText.DisplayedString = this.pText;
                        }
                        
                    }

                }
                else
                {
                    this.pText += "*";
                    this.text += args.Unicode;
                    if (!password)
                    {
                        this.displayText.DisplayedString = this.text;
                        this.displayText.DisplayedString += "|";
                    } else
                    {
                        this.displayText.DisplayedString = pText;
                        this.displayText.DisplayedString += "|";
                    }
                   
                }
                
            } else
            {
                if (!password)
                {
                    this.displayText.DisplayedString = text;
                }
            }
            
            Console.WriteLine(this.text);
        }

        public void Update()
        {
            
        }
        public void Draw()
        {
            if (ui.visible == true)
            {
                window.Draw(this.tBox);
                window.Draw(this.displayText);
            }
        }

    }
}
