using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ConsoleApplication.MenuControllers;
using ConsoleApplication.Settings;
using Microsoft.Extensions.Logging;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ConsoleApplication
{
    public class MenuSelection
    {
        public MenuSelection(MenuItem menuItem, MemberInfo methodInfo, Type menuControllerType)
        {
            MenuItem = menuItem;
            MenuControllerType = menuControllerType;
            MethodInfo = methodInfo;
        }
        public readonly MenuItem MenuItem;
        public readonly Type MenuControllerType;
        public readonly MemberInfo MethodInfo;
    }
    public class Menu
    {
        private List<MenuSelection> MenuOptions { get; set; }

        private readonly ILogger<App> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Menu(ILogger<App> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            MenuOptions = GetMenuOptions();
        }

        public async Task DisplayMenuOptions()
        {
            while (true)
            {
                MenuOptions.ForEach(menuSelection => Console.WriteLine(menuSelection.MenuItem.GetName()));
                var dataIn = Console.ReadLine();
                try
                {
                    MenuSelection item = MenuOptions.Find(menuSelection => menuSelection.MenuItem.GetName() == dataIn);
                    var controller = _serviceProvider.GetService(item.MenuControllerType);                    
                    // Grab that Controller out of our services collection and invoke the corresponding method
                    if(controller != null){
                        controller.GetType().InvokeMember(item.MethodInfo.Name.ToString(),System.Reflection.BindingFlags.InvokeMethod,null,controller, null);
                    }
                    // Call the method related to this menu item
                }
                catch (Exception e)
                {

                }

            }

        }

        private List<MenuSelection> GetMenuOptions()
        {
            List<MenuSelection> CurrentMenuOptions = new List<MenuSelection>();
            Type type = typeof(BaseMenuController);
            List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)).ToList();

            types.ForEach(MenuControllerType =>
            {
                MemberInfo[] myMembers = MenuControllerType.GetMembers();
                for (int i = 0; i < myMembers.Length; i++)
                {
                    if (myMembers[i].MemberType == MemberTypes.Method)
                    {
                        MemberInfo MemberInfo = myMembers[i];
                        System.Attribute[] attrs = System.Attribute.GetCustomAttributes(MemberInfo);
                        foreach (System.Attribute attr in attrs)
                        {
                            if (attr is MenuItem)
                            {
                                MenuItem a = (MenuItem)attr;
                                CurrentMenuOptions.Add(new MenuSelection(a, MemberInfo, MenuControllerType));
                            }
                        }
                    }
                }
            });
            return CurrentMenuOptions;
        }

    }
}