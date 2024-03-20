using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Player
{

    private const float PLAYER_VELOCITY = 5F;
    Vector2 _position;
    Texture2D _texture;
    Keys _keyUp;
    Keys _keysDown;
    Game _game; // declarado para usar a GraficsDevice e pegar a viewport

    public Player(Game game, Vector2 position, Texture2D texture, Keys keyUp, Keys keyDown)
    {
        _game = game;
        _position = position;
        _texture = texture;
        _keyUp = keyUp;
        _keysDown = keyDown;
    }

    public void Update()
    {
        var keyboard = Keyboard.GetState();

        if(keyboard.IsKeyDown(_keyUp))//IsKeyDown verifica se a tecla está precionada. IsKeyUP verifica se a tecla não está precionada.
        {
            _position.Y -= PLAYER_VELOCITY;
        }
        else if (keyboard.IsKeyDown(_keysDown))
        {
            _position.Y += PLAYER_VELOCITY;
        }

        // Colisão das barras com os limites verticais
        var viewport = _game.GraphicsDevice.Viewport;

        if(_position.Y < 0)
        {
            _position.Y = 0.0f;
        }
        else if(_position.Y + _texture.Height > viewport.Height)
        {
            _position.Y = viewport.Height - _texture.Height;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}
