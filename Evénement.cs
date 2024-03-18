using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestion_Evénement
{
    public partial class Evénement : Form
    {
        public Evénement()
        {
            InitializeComponent();


        }

        private void Evénement_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'gestionEvenementsDataSet.Evenements'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.evenementsTableAdapter.Fill(this.gestionEvenementsDataSet.Evenements);

            RemplirComboBox();
            LoadEvents();
        }
        private void RemplirComboBox()
        {

            SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=GestionEvenements;Integrated Security=True");
            connection.Open();


            string sql = "SELECT Type FROM Evenements";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    string valeur = reader[0].ToString();


                    cbType.Items.Add(valeur);
                }

                reader.Close();
            }

            connection.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var context = new GestionEvenement())
            {

                if (string.IsNullOrEmpty(txtLibelle.Text) || string.IsNullOrEmpty(cbType.SelectedItem.ToString()) ||
                    string.IsNullOrEmpty(txtLieu.Text) || string.IsNullOrEmpty(txtOrganisateur.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                Evenement evenement = new Evenement
                {
                    Libelle = txtLibelle.Text,
                    Description = txtDescription.Text,
                    Type = cbType.SelectedItem.ToString(),
                    DateDebut = dtpDébut.Value,
                    DateFin = dtpFin.Value,
                    Lieu = txtLieu.Text,
                    Organisateur = txtOrganisateur.Text,
                    SiteWeb = txtSiteWeb.Text
                };


                context.Evenements.Add(evenement);


                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Evenement ajouté avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtLibelle.Text = "";
                    txtDescription.Text = "";
                    cbType.SelectedIndex = 0;
                    dtpDébut.Value = DateTime.Today;
                    dtpFin.Value = DateTime.Today;
                    txtLieu.Text = "";
                    txtOrganisateur.Text = "";
                    txtSiteWeb.Text = "";
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de l'ajout de l'événement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new GestionEvenement())
            {
                if (string.IsNullOrEmpty(txtLibelle.Text) || string.IsNullOrEmpty(cbType.SelectedItem.ToString()) ||
            string.IsNullOrEmpty(txtLieu.Text) || string.IsNullOrEmpty(txtOrganisateur.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                int selectedEventId = GetSelectedEventId(); 

                Evenement evenement = context.Evenements.Find(selectedEventId);

                if (evenement == null)
                {
                    MessageBox.Show("Aucun événement n'est sélectionné pour la modification.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                evenement.Libelle = txtLibelle.Text;
                evenement.Description = txtDescription.Text;
                evenement.Type = cbType.SelectedItem.ToString();
                evenement.DateDebut = dtpDébut.Value;
                evenement.DateFin = dtpFin.Value;
                evenement.Lieu = txtLieu.Text;
                evenement.Organisateur = txtOrganisateur.Text;
                evenement.SiteWeb = txtSiteWeb.Text;

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Evenement modifié avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    ClearFormFields();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la modification de l'événement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearFormFields()
        {
            txtIdEvenement.Text = "";
            txtLibelle.Text = "";
            txtDescription.Text = "";
            cbType.SelectedIndex = 0;
            dtpDébut.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            txtLieu.Text = "";
            txtOrganisateur.Text = "";
            txtSiteWeb.Text = "";
        }
        private int GetSelectedEventId()
        {
            if (dgvEvenement.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEvenement.SelectedRows[0];
                return int.Parse(selectedRow.Cells[0].Value.ToString());
            }
            else
            {
                return -1; 
            }
        }
        private void LoadEvents()
        {
            var context= new GestionEvenement();
            dgvEvenement.DataSource = context.Evenements.ToList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var context = new GestionEvenement();
            int selectedEventId = GetSelectedEventId(); 

            Evenement evenement = context.Evenements.Find(selectedEventId);

            if (evenement == null)
            {
                MessageBox.Show("Aucun événement n'est sélectionné pour la suppression.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet événement ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                context.Evenements.Remove(evenement);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Evenement supprimé avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    ClearFormFields();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la suppression de l'événement : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEvenement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var context = new GestionEvenement();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvEvenement.Rows[e.RowIndex];
                Evenement evenement = selectedRow.DataBoundItem as Evenement;
                if (evenement != null)
                {
                    txtIdEvenement.Text = evenement.IdEvenement.ToString();
                    txtLibelle.Text = evenement.Libelle;
                    txtDescription.Text = evenement.Description;
                    cbType.SelectedItem = evenement.Type;
                    dtpDébut.Value = evenement.DateDebut;
                    dtpFin.Value = evenement.DateFin;
                    txtLieu.Text = evenement.Lieu;
                    txtOrganisateur.Text = evenement.Organisateur;
                    txtSiteWeb.Text = evenement.SiteWeb;

                    txtIdEvenement.Enabled = false;
                    txtLibelle.Enabled = true;
                    txtDescription.Enabled = true;
                    cbType.Enabled = true;
                    dtpDébut.Enabled = true;
                    dtpFin.Enabled = true;
                    txtLieu.Enabled = true;
                    txtOrganisateur.Enabled = true;
                    txtSiteWeb.Enabled = true;
                }
            }
        }
    }
}
