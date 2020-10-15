using System;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ConsoleApplication.MenuControllers
{
    public class MainMenuController : BaseMenuController
    {
        [MenuItem("Read Data")]
        public void ReadData()
        {
            Console.Write(" We Freaking Did it!!!!! Read");
        }
        [MenuItem("Read Data Again")]
        public void ReadDataAgain()
        {

            Console.Write(" We Freaking Did it!!!!! ReadAgain");
        }
        [MenuItem("And Again")]
        public void ReadDataAndAgain()
        {

            Console.Write(" We Freaking Did it!!!!! And Again");
        }
    }

}