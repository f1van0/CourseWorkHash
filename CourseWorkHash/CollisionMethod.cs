﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWorkHash
{
    //Перечисление методов разрешения коллизий
    public enum CollisionMethod
    {
        Chains = 1,
        BinaryTree,
        LinearProbing,
        QuadraticProbing,
        DoubleHashing
    }
}