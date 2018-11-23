
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

using SFML.Graphics;
using SFML.Window;


namespace ToT
{
    class UIWindow
    {
        RenderWindow win;

        RectangleShape winContent;
        RectangleShape titleBar;
        Text winTitle;
        public bool visible = true;
        public int winX, winY;

        public UIWindow(RenderWindow w, int width, int height, int x, int y, string title)
        {
            winX = x;
            winY = y;

            win = w;
            winContent = new RectangleShape(new SFML.System.Vector2f(width, height));
            titleBar = new RectangleShape(new SFML.System.Vector2f(width, 20));

            titleBar.Position = new SFML.System.Vector2f(x, y);
            winContent.Position = new SFML.System.Vector2f(x, y + titleBar.Size.Y);

            titleBar.FillColor = new Color(204, 166, 102) ;
            winContent.FillColor = new Color(255, 218, 155);
            winTitle = new Text(title, new Font("Tahoma.ttf"), 12);
            winTitle.Color = Color.Black;
            winTitle.Position = new SFML.System.Vector2f(x+5, y+2);
        }

        public void Update()
        {



        }

        public void Draw()
        {
            if (visible)
            {
                win.Draw(titleBar);
                win.Draw(winContent);

                win.Draw(winTitle);
            }
        }

    }
}
