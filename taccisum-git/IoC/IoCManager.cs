/********************************************************************************************
 * @author tac 
 * @date 2016/08/25
 * @see Microsoft.Practices.Unity
 * @desc
 * Unity是通过配置xml实现IoC的，因此更加擅长一些需要在运行时修改接口的注入类型的实现
 * 若纯粹为了解耦合应倾向于用Mef来实现（无需配置xml，更加简单易用）
 ********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace IoC.Manager
{
    /// <summary>
    /// 基于Unity实现的IoC
    /// todo::考虑单独抽取出Unity的IoC实现，因为考虑到各种IoC实现方式的不同，不太可能抽象为统一的接口进行管理 
    /// </summary>
    public class IoCManager
    {
        private static IoCManager _instance;

        public static IoCManager GetInstance()
        {
            if(_instance==null)
                _instance = new IoCManager();
            return _instance;
        }

        public IIoC Create()
        {
            return new ImplementWithUnity();
        }

        /// <summary>
        /// 基于Unity实现的IoC
        /// </summary>
        private class ImplementWithUnity : IIoC
        {
            public T Resolve<T>()
            {
                //todo::应对container作缓存，以提高性能
                IUnityContainer container = new UnityContainer();
                var section = GetSection();
                container.LoadConfiguration(section);
                T instance = container.Resolve<T>();
                return instance;
            }

            private UnityConfigurationSection GetSection()
            {
                return (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            }
        }
    }



}
