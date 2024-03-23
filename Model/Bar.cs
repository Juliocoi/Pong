using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Model;

public class Bar
{

    private const float SPEED = 5F;
    private Vector2 _position;
    private Texture2D _texture;
    private Keys _keyUp;
    private Keys _keysDown;
    private Game _game; // declarado para usar a GraficsDevice e pegar a viewport

    public Bar(Game game, Vector2 position, Texture2D texture, Keys keyUp, Keys keyDown)
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

        if (keyboard.IsKeyDown(_keyUp))//IsKeyDown verifica se a tecla está precionada. IsKeyUP verifica se a tecla não está precionada.
        {
            _position.Y -= SPEED;
        }
        else if (keyboard.IsKeyDown(_keysDown))
        {
            _position.Y += SPEED;
        }

        // Colisão das barras com os limites verticais
        var viewport = _game.GraphicsDevice.Viewport;

        if (_position.Y < 0)
        {
            _position.Y = 0.0f;
        }
        if (_position.Y + _texture.Height > viewport.Height)
        {
            _position.Y = viewport.Height - _texture.Height;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    //Coalisão da bola com a barra
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
    }

    public void HasCollide(Ball ball)
    {
        Rectangle _ballBounds = ball.GetBounds();
        Rectangle _barBounds = GetBounds();

        if (_barBounds.Intersects(_ballBounds))
        {
            if(ball.BallDirection.X < 0)
            {
                ball.SetPosition(_position.X + _texture.Width);
            }
            else
            {
                ball.SetPosition(_position.X - _texture.Width);
            }

            ball.InvertBallDirection();
        }
    }
}
