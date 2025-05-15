using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


//https://github.com/ckdghks5179/clue_game
//https://github.com/ckdghks5179/clue_game.git


namespace Clue
{
    public partial class Form1 : Form
    {
        Form2 notePad;

        private GameState gameState;
        private int playerId;
        private PictureBox myPlayerBox;
        private Dictionary<int, PictureBox> playerBoxes = new Dictionary<int, PictureBox>();
        //private Point[,] clue_map_point;
        private int[,] clue_map => gameState.clue_map;

        private Player[] playerList => gameState.Players;
        private Player player;
        List<Card> cardList = new List<Card>();

        private int RollDice()
        {
            Random random = new Random();
            return random.Next(2, 13);
        }

        public Form1(GameState gamestate1, int playerId)
        {
            InitializeComponent();
            //InitializeClueMap_Point();
            //InitializeClueMap();
            //OpenPlayerChooseForm();
            this.gameState = gamestate1;
            this.playerId = playerId;
            this.player = playerList[playerId];
        }

        private void UpdateControlState()
        {
            bool isMyTurn = gameState.CurrentTurn == playerId;
            btnRoll.Enabled = isMyTurn;
            btnTurnEnd.Enabled = isMyTurn;

            /* btnUp.Enabled = isMyTurn;
             btnDown.Enabled = isMyTurn;
             btnLeft.Enabled = isMyTurn;
             btnRight.Enabled = isMyTurn;
             btnTurnEnd.Enabled = isMyTurn;
            */
        }
        private Color GetPlayerColor(int id)
        {
            Color[] colors = new Color[] { Color.Green, Color.Red, Color.Blue, Color.Purple, Color.Orange, Color.White };
            return colors[id % colors.Length];
        }

        public void UpdatePlayerPositions()
        {
            for (int i = 0; i < gameState.TotalPlayers; i++)
            {
                var p = gameState.Players[i];
                if (playerBoxes.ContainsKey(i))
                {
                    playerBoxes[i].Location = gameState.clue_map_point[p.x, p.y];
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            for (int i = 0; i < gameState.TotalPlayers; i++)
            {
                Player p = gameState.Players[i];

                PictureBox playerBox = new PictureBox
                {
                    Name = $"playerBox{i}",
                    Size = new Size(20, 20),
                    BackColor = GetPlayerColor(i), // 각 플레이어마다 다른 색상
                    Location = gameState.clue_map_point[p.x, p.y],
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                playerBoxes[i] = playerBox;

                this.Controls.Add(playerBox);


                playerBox.BringToFront();
                // ⬇️ 현재 Form이 담당하는 플레이어라면 저장
                if (i == playerId)
                {
                    myPlayerBox = playerBox;
                }
            }

            UpdateControlState();

            
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            int diceValue = RollDice();
            dice1.Text = diceValue.ToString();
            lbRemain.Text = diceValue.ToString();
            btnRoll.Enabled = false;
        }

        private void TryMove(int dx, int dy)
        {
            if (int.Parse(lbRemain.Text) <= 0) return;

            int newX = player.x + dx;
            int newY = player.y + dy;

            if (newX < 0 || newX >= 25 || newY < 0 || newY >= 24) return;
            if (gameState.clue_map[newX, newY] == 1) return;

            gameState.clue_map[player.x, player.y] = 0;
            player.x = newX;
            player.y = newY;
            gameState.clue_map[newX, newY] = 3;
            playerBoxes[playerId].Location = gameState.clue_map_point[newX, newY];
            lbRemain.Text = (int.Parse(lbRemain.Text) - 1).ToString();
        }


        private void btnUp_Click(object sender, EventArgs e)
        {

            TryMove(-1, 0);
            

        }   
        private void btnDown_Click(object sender, EventArgs e)
        {

            TryMove(1, 0);
           
        }

        private void btnRight_Click(object sender, EventArgs e)
        {

            TryMove(0, 1);
           
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            TryMove(0, -1);
           
        }

        private void btnTurnEnd_Click(object sender, EventArgs e)
        {
            //btnRoll.Enabled = true;
            lbRemain.Text = "0";
            gameState.AdvanceTurn();
            foreach (var form in PlayerChoose.AllPlayerForms)
            {
                form.UpdateControlState();
                form.UpdatePlayerPositions();
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            // 각 플래이어마다 다른 메모장 열기
            notePad = new Form2();
            notePad.Show();
        }
    }
}
