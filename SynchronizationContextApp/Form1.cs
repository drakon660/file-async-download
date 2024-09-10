using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronizationContextApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task button1_Click(object sender, EventArgs e)
        {
            await CauseDeadlockAsync();
        }


        private Task CauseDeadlockAsync()
        {
            // Start a new task (simulating some asynchronous operation, like an HTTP request).
            var task = Task.Run(() =>
            {
                // Simulate a delay in the asynchronous operation.
                Task.Delay(5000).Wait();
            });

            // Improperly wait on the asynchronous operation synchronously.
            // This will cause a deadlock when the continuation of the awaited operation
            // tries to post back to the UI thread, but the UI thread is blocked waiting for the task.
            task.Wait();

            return task;
        }
    }
}