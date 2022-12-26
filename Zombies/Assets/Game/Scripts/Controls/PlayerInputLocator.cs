namespace Game.Scripts.Controls
{
    public class PlayerInputLocator
    {
        private static PlayerInput PlayerInput { get; } = new PlayerInput();

        public static PlayerInput GetPlayerInput()
        {
            return PlayerInput;
        } 
    }
}