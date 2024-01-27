using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Services 
{
	private static Services instance;
	public static Services Instance
	{
		get 
		{ 
			if (instance == null)
			{
				instance = new Services();
			}
			return instance; 
		}
	}

	public IList<IServiceAble> ServiceAbles { get; private set; } = new List<IServiceAble>();

	public void AddService(IServiceAble service)
	{
		ServiceAbles.Add(service);
	}

	public T GetService<T>()
	{
		var typeOfT = typeof(T);
		var result = ServiceAbles.Where(s => s.GetType() == typeof(T)).First();
		return (T)result;
	}
}
