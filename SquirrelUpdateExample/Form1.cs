using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace SquirrelUpdateExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //var repoUrl = "https://github.com/DeryaginAlex/SquirrelTest";
                var repoUrl = "https://github.com/meJevin/WPFFrameworkTest";

                using (var updateManager = await UpdateManager.GitHubUpdateManager(repoUrl))
                {
                    // Проверка наличия обновлений
                    var updateInfo = await updateManager.CheckForUpdate();

                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        // Если есть обновления, скачиваем и устанавливаем их
                        MessageBox.Show("Обновление найдено! Устанавливаем...", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await updateManager.UpdateApp();

                        // Перезапуск приложения после обновления
                        MessageBox.Show("Приложение будет перезапущено для применения обновления.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateManager.RestartApp();
                    }
                    else
                    {
                        MessageBox.Show("У вас установлена последняя версия приложения.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
