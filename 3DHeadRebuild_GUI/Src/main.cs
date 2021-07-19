/*****************************************************************************************
 * File Name     ： main.cs
 * Description   ： 三维人头重建软件主入口
 * Author        ： Zhanli@Uestc
 * Update Time   ： 2019-03-11
 *****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace ThreeDHeadRebuild
{
    static class main
    {
        /****************************************************************
         * 应用程序的主入口点。
         ****************************************************************/ 
        [STAThread]
        static void Main()
       {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MiniForm());

            Application.Run(new MainForm());
        }
    }
}
