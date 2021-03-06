﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using System.Data.Entity;
using PISController1.Controller;
using PISController1;

namespace PISLawyerView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMainLawyer>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<DbContext, PISDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerArchiving>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerClient>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerContractClient>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerDiagrams>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerMain>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerPrinting>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerReporting>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerSelection>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerService>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ControllerValidation>(new
           HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
