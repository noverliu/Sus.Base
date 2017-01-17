using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Infrastructure
{
    /// <summary>
    /// 构建一个单例对象贯穿整个应用生命周期
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <remarks>异步访问实例</remarks>
    public class Singleton<T> : Singleton
    {
        static T instance;

        /// <summary>泛型T的实例，每个类型只能有一个单例实例</summary>
        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// 为已知类型提供一个单例列表
    /// </summary>
    /// <typeparam name="T">列表的类型</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>泛型T的实例，每个类型只能有一个单例实例</summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    /// <summary>
    /// 按照指定类型的Key和Value构建一个单例字典
    /// </summary>
    /// <typeparam name="TKey">Key的类型</typeparam>
    /// <typeparam name="TValue">Value的类型</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>泛型T的实例，每个类型只能有一个单例实例</summary>
        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }

    /// <summary>
    /// 提供一个字典用于访问 <see cref="Singleton{T}"/>.
    /// </summary>
    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> allSingletons;

        /// <summary>类型与实例的字典.</summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }
}
