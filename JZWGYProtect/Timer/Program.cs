using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 100; i++)
            //{

            //    SendMessage("131", "131");
            //}
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(aTimer_Elapsed);
            // 设置引发时间的时间间隔 此处设置为１秒（１０００毫秒）
            aTimer.Interval = 6000;  //设置时间间隔
                                     // aTimer.AutoReset = false;
          
                aTimer.Enabled = true;
            
            Console.WriteLine("按回车键结束程序");
            Console.WriteLine("等待程序的执行．．．．．．");
            Console.WriteLine();
            Console.ReadLine();
            //SendMessage("131", "131");
        }
       

        static void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendMessage("131", "131");

        }


        private static void SendMessage(string PhoneNum, string Message)
        {
            try
            {
                //if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 31)
                //{
                    Console.WriteLine("2222222222222222222222222222");
                    Console.WriteLine("1111111111111111111111111111");


                //}


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
        }
     
    }




    }
