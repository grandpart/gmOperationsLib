using Zephry;

namespace Grandmark
{
    public delegate void UserDelegate<T>(Connection aConnection, UserKey aUserKey, T aZephob);
}