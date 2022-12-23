using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUILibrary
{
    public class PlayerController : MonoBehaviour
    {
        public Player Player { get; init; }
        public int UpdateTime { get; init; }

        public override void Update()
        {
            PlayerMoveTowards();
            CheckRotate();
            ChangeSpeed();
            CheckGravity();
        }

        public override void FixedUpdate()
        {
            //TODO: timer is lagging with small intervalMs, need to fix and after all physics movement from Update paste here.
        }

        private void PlayerMoveTowards()
        {
            Player.GameObject.Controller.TranslateTo(Player.GameObject.Controller.TranslationX + Player.Speed * Math.Cos(Player.GameObject.Controller.Rotation * Math.PI / 180) * Time.Instance.DeltaTime,
                Player.GameObject.Controller.TranslationY + Player.Speed * Math.Sin(Player.GameObject.Controller.Rotation * Math.PI / 180) * Time.Instance.DeltaTime);
        }

        private void CheckRotate()
        {
            if (Player.Rotation == 1)
                Player.GameObject.Controller.RotateTo(Player.GameObject.Controller.Rotation - Player.AngleChange);
            else if (Player.Rotation == -1)
                Player.GameObject.Controller.RotateTo(Player.GameObject.Controller.Rotation + Player.AngleChange);
        }

        private void ChangeSpeed()
        {
            var rotation = Player.GameObject.Controller.Rotation + Math.Ceiling(-Player.GameObject.Controller.Rotation / 360) * 360;
            if (rotation >= 0 && rotation <= 180 && Player.Speed < Player.MaxSpeed && !Player.IsFreezing)
                Player.Speed += Player.SpeedBoost;
            else if (rotation > 190 && rotation < 350 && Player.Speed > 0)
                Player.Speed = Math.Max(0, Player.Speed + Math.Sin(rotation * Math.PI / 180) / 2 * Player.SpeedBoost);
        }

        private void CheckGravity()
        {
            if (Player.Speed <= 0 || Player.IsFreezing)
            {
                Player.GameObject.Controller.TranslateTo(Player.GameObject.Controller.TranslationX, Player.GameObject.Controller.TranslationY + 9.8 * 100 * Time.Instance.DeltaTime);

                var rotation = Player.GameObject.Controller.Rotation + Math.Ceiling(-Player.GameObject.Controller.Rotation / 360) * 360;
                if (rotation > 100 && rotation <= 270)
                    Player.GameObject.Controller.RotateTo(Player.GameObject.Controller.Rotation - Player.AngleChange);
                else if (rotation > 270 || (rotation > 0 && rotation < 70))
                    Player.GameObject.Controller.RotateTo(Player.GameObject.Controller.Rotation + Player.AngleChange);

            }
        }
    }
}
