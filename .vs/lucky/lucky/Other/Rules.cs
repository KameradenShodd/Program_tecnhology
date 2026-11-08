using System;
using System.Collections.Generic;
using System.Text;

namespace lucky.Other
{
    public record Rules(int min = 1, int max = 100, int max_invalids = 3);
}
