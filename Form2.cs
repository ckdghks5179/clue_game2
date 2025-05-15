using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clue
{
    public partial class Form2 : Form
    {
        Player currentPlayer;
        public Form2()
        {
            InitializeComponent();
            //currentPlayer = player;

            //LoadCheckedCards();
        }

        /// 메모장에 체크한거 저장
        public void SaveCheckedCards()
        {
            // Save the checked cards to the current player's properties
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                currentPlayer.checkedMans[i] = checkedListBox1.GetItemChecked(i);

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
                currentPlayer.checkedWeapons[i] = checkedListBox2.GetItemChecked(i);

            for (int i = 0; i < checkedListBox3.Items.Count; i++)
                currentPlayer.checkedRooms[i] = checkedListBox3.GetItemChecked(i);
        }
        public void LoadCheckedCards()
        {
            // Load the checked cards from the current player's properties
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, currentPlayer.checkedMans[i]);
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
                checkedListBox2.SetItemChecked(i, currentPlayer.checkedWeapons[i]);
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
                checkedListBox3.SetItemChecked(i, currentPlayer.checkedRooms[i]);
        }
    }
}
