﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Lib.Domain.SmallApi
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class ReadOnlyAttribute : Attribute
  {
  }
}
