﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Enum
{
    public enum FieldType
    {
        INT = 1,
        STRING = 2,
        BOOL = 3,
        DATE = 4,
        TIME = 5,
        DATETIME = 6,
    }

    public enum FormControllerType
    {
        INPUT = 1,
        SELECT = 2,
        TEXTAREA = 3,
        RADIO = 4,
        CHECK = 5,
        PICKER = 6,

    }

    public enum ListColumnType
    {

    }

    public enum InterfaceParamType
    {
        IN = 1,
        OUT = 2
    }
}