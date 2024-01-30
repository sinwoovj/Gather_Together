using DI;
using UnityEngine;

public class DIMono: MonoBehaviour
{
	InjectObj InjectObj = new InjectObj();


    public void CheckInjection()
    {   
        InjectObj.CheckAndInject(this);
    }

    private void Start()
    {
        CheckInjection();
        Initialize();
    }

    protected virtual void Initialize()
	{

	}

}