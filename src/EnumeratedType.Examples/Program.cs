﻿using EnumeratedType.Examples;

Console.WriteLine("Hello, World!");

var value = EnumeratedTypeEnum.EnumeratedValue.Extend();

Console.WriteLine($"Order: {value.Order}");
Console.WriteLine($"Aliases: {string.Join(", ", value.Aliases.Value ?? [])}");
Console.WriteLine($"Description: {value.Description}");
Console.WriteLine($"Category: {value.Category}");
Console.WriteLine($"Default Value: {value.DefaultValue}");
Console.WriteLine($"Is Read Only: {value.IsReadOnly}");