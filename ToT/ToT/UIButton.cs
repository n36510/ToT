using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ToT
{
    class UIButton
    {
        RectangleShape btn;
        Text btnText;
        int x, y;
        RenderWindow window;
        Action action;

        private void doAction(Action func)
        {
            func();
            Console.WriteLine("Button Clicked.");
        }
        public UIButton(RenderWindow win, UIWindow w, int xx, int yy, int width, int height, Action doLogin, string txt)
        {
            x = w.winX + xx;
            y = w.winY + yy;
            btn = new RectangleShape(new SFML.System.Vector2f(width, height));
            btn.FillColor = new Color(193, 139, 44);
            btn.Position = new SFML.System.Vector2f(x, y);

            btnText = new Text(txt, new Font("Tahoma.ttf"), 12);
            btnText.Color = Color.Black;
            btnText.DisplayedString = txt;
            btnText.Position = new SFML.System.Vector2f(x+(width/2.95f), y+(height/2.95f));

            win.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(MouseInput);
            window = win;
            action = doLogin;
        }

        void MouseInput(object sender, MouseButtonEventArgs args)
        {
            FloatRect pos = new FloatRect(this.x, this.y, this.btn.GetGlobalBounds().Width, this.btn.GetGlobalBounds().Height);
            if (pos.Contains(args.X, args.Y))
            {
                doAction(action);
            }
        }

        public void Draw()
        {
            window.Draw(btn);
            window.Draw(btnText);
        }
    }
}
