using System;
using System.Data.SQLite;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/

namespace Projet_applications
{
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App() {Database = Database.getInstance(), Running = true};
            while (app.Running)
            {
                app.Start();
            }
            CustomConsole.PrintSuccess("Au revoir !");
        }
    }
}