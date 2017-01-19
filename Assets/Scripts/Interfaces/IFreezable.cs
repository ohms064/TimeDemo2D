namespace Assets.Scripts.Interfaces {
    public interface IFreezable {
        void FrozenRotation( float rotation );
        void Freeze();
        void Unfreeze();
    }
}
