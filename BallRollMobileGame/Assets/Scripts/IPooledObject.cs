/*****************************************************************************
// File Name :         IPooledObject.cs
// Author :            Kyle Grenier
// Creation Date :     06/20/2021
//
// Brief Description : A contract a pooled object may choose to subscribe to.
*****************************************************************************/
public interface IPooledObject
{
    void OnSpawn();
    string GetPoolTag();
}