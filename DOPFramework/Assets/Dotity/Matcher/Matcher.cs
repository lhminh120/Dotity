
using Dotity;

public class Matcher : IMatcher
{
    #region Static Function
    public static Matcher AnyOf(params ComponentKey[] keys)
    {
        return new Matcher(keys, null);
    }
    public static Matcher NoneOf(params ComponentKey[] keys)
    {
        return new Matcher(null, keys);
    }
    #endregion
    #region Function
    private ComponentKey[] _anyOf;
    private ComponentKey[] _noneOf;
    private Matcher(ComponentKey[] anyOf, ComponentKey[] noneOf)
    {
        _anyOf = anyOf;
        _noneOf = noneOf;
    }

    public void WithoutOf(params ComponentKey[] noneOf)
    {
        _noneOf = noneOf;
    }
    public bool Equal(IMatcher matcher)
    {
        bool check;
        ComponentKey[] anyOf = matcher.GetAnyOf();
        if (anyOf != null || _anyOf != null)
        {
            if ((anyOf == null && _anyOf != null) || (anyOf != null && _anyOf == null)) return false;
            if (anyOf.Length != _anyOf.Length) return false;
            for (int i = 0, length = anyOf.Length; i < length; i++)
            {
                check = false;
                for (int j = 0; j < length; j++)
                {
                    if (anyOf[i] == _anyOf[j])
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) return false;
            }
        }
        
        ComponentKey[] noneOf = matcher.GetNoneOf();
        if (noneOf != null || _noneOf != null)
        {
            if ((noneOf == null && _noneOf != null) || (noneOf != null && _noneOf == null)) return false;
            if (noneOf.Length != _noneOf.Length) return false;
            for (int i = 0, length = noneOf.Length; i < length; i++)
            {
                check = false;
                for (int j = 0; j < length; j++)
                {
                    if (noneOf[i] == _noneOf[j])
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) return false;
            }
        }
        return true;
    }

    public ComponentKey[] GetAnyOf() => _anyOf;

    public ComponentKey[] GetNoneOf() => _noneOf;
    public bool Match(IEntity entity)
    {
        return (_anyOf == null || entity.HasComponents(_anyOf)) && (_noneOf == null || !entity.HasComponents(_noneOf));
    }
    #endregion

}
