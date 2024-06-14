using System;
using System.Collections.Generic;

namespace Pks4Core;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string ThirdName { get; set; } = null!;
}
