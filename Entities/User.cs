
using BankSystem.Constants;

namespace BankSystem.Entities;
    public class User
    {
        enMode _Mode;
        string _UserName;
        string _Password;
        int _Permissions;

        bool _MarkedForDelete = false;

   
    }