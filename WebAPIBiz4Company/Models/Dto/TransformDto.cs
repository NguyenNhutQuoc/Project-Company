using System.Reflection;

namespace WebAPIBiz4Company.Models.Dto;

public class TransformDto
{
    public static TY ToObject<T, TY>(T t,TY y) where T : new() where TY : new()
    { 
       TY newObject = new TY();
        int index = 0;
        if (t is not null && y is not null)
        {
            foreach (PropertyInfo property in t.GetType().GetProperties())
            {
                if (property.GetValue(t) is null)
                {
                    PropertyInfo properOldUser = y.GetType().GetProperties()[index];
                    newObject.GetType().GetProperties()[index].SetValue(newObject, properOldUser.GetValue(y));
                }
                else
                {
                    newObject.GetType().GetProperties()[index].SetValue(newObject, property.GetValue(t));
                }
                index++;
            }
        }

        return newObject;
    }
}