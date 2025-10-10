namespace Domain.Gameplay
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
        
        public Vector3(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }
        
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}