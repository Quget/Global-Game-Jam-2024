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
				instance.CreateServices();
			}
			return instance; 
		}
	}

	public IList<IServiceAble> ServiceAbles { get; private set; } = new List<IServiceAble>();

	private void CreateServices()
	{
		Instance.AddService(new GameValueService());
		Instance.AddService(new AssignmentService());
	}

	private void AddService(IServiceAble service)
	{
		foreach (var item in ServiceAbles)
		{
			if (item.GetType() == service.GetType())
				return;
		}
		ServiceAbles.Add(service);

	}

	public T GetService<T>()
	{
		var typeOfT = typeof(T);
		var result = ServiceAbles.Where(s => s.GetType() == typeof(T)).FirstOrDefault();
		return (T)result;
	}
}
