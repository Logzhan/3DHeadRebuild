using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDHeadRebuild.Src.Util
{
    public struct Vec3f
    {
        public  float x, y, z;
        public Vec3f(float x, float y, float z) {
            this.x = x;this.y = y;this.z = z;
        }
    }
    public struct Vec2f
    {
        public  float x, y;
    }

    public class Vec2i {
        public int x, y;
    }

    public class Vec3i {
        public int x, y, z;
        public int[] toArray() {
            int[] arr = new int[3];
            arr[0] = x;
            arr[1] = y;
            arr[2] = z;
            return arr;
        }
    }
}
