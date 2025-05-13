using djahuacoTS5.Models;

namespace djahuacoTS5;

public partial class MainPage : ContentPage
{
    Persona personaSeleccionada;

    public MainPage()
    {
        InitializeComponent();
        CargarDatos();
    }

    private async void CargarDatos()
    {
        personasListView.ItemsSource = await App.Database.ObtenerPersonasAsync();
        LimpiarCampos();
    }

    private void LimpiarCampos()
    {
        nombreEntry.Text = string.Empty;
        correoEntry.Text = string.Empty;
        personaSeleccionada = null;
        guardarButton.Text = "Guardar";
        cancelarButton.IsVisible = false;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nombreEntry.Text) || string.IsNullOrWhiteSpace(correoEntry.Text))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            return;
        }

        var persona = personaSeleccionada ?? new Persona();
        persona.Nombre = nombreEntry.Text;
        persona.Correo = correoEntry.Text;

        await App.Database.GuardarPersonaAsync(persona);
        await DisplayAlert("Éxito", personaSeleccionada != null ? "Persona actualizada" : "Persona guardada", "OK");

        CargarDatos();
    }

    private void OnSeleccionChanged(object sender, SelectionChangedEventArgs e)
    {
        personaSeleccionada = (Persona)e.CurrentSelection.FirstOrDefault();

        if (personaSeleccionada != null)
        {
            nombreEntry.Text = personaSeleccionada.Nombre;
            correoEntry.Text = personaSeleccionada.Correo;
            guardarButton.Text = "Actualizar";
            cancelarButton.IsVisible = true;
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var persona = (Persona)button.CommandParameter;

        bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar a {persona.Nombre}?", "Sí", "No");
        if (confirm)
        {
            await App.Database.EliminarPersonaAsync(persona);
            await DisplayAlert("Eliminado", "Persona eliminada con éxito", "OK");
            CargarDatos();
        }
    }

    private void OnCancelarClicked(object sender, EventArgs e)
    {
        personasListView.SelectedItem = null;
        LimpiarCampos();
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var persona = (Persona)button.CommandParameter;

        if (persona != null)
        {
            await DisplayAlert("Editar", $"Editando a {persona.Nombre}", "OK"); // Confirmación de que se presionó
            personaSeleccionada = persona;
            nombreEntry.Text = persona.Nombre;
            correoEntry.Text = persona.Correo;
            guardarButton.Text = "Actualizar";
            cancelarButton.IsVisible = true;
        }
    }
}
