# WPFTabs

## Descrizione
#### Esercizio WPF in C# da 5 tab diverse (Bubble, Collatz, Telefono, Acronimo, Isogramma). Ogni tab avrà una sua funzione che verrà eseguita indipendentemente (anche a tab non aperta) e disporrà inoltre di una descrizione della propria funzione e di un footer contenente data e ora.


## Griglia
Per prima cosa bisognerà cerare una griglia che andrà a contenere tab e footer, di modo da non dover creare un footer identico per ogni tab e poter riutilizzare sempre lo stesso sempliccemente posizionandolo in basso.
        
```xaml
<Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition Height="50"></RowDefinition>
        </Grid.RowDefinitions>
```

Il primo tag <b><i><</i></b><b><i>Grid</i></b><b><i>></i></b> andrà chiuso una volta sviluppata la partre grafica anche delle tab, dato che sarà la griglia che conterrà l'intera applicazione. Delle due righe all'interno di <b><i><Grid.RowDefinitions></i></b> invece, la prima andrà a contenere le diverse tab e la seconda il footer, proprio per questo essa verrà ridimensionata, di modo da non far occupare troppo spazio al nostro footer. 


## Footer
Dopo aver creato la prima griglia, il prossimo componente da creare sarà il footer, comune per tutte le diverse tab; come prima cosa ne osserveremo la struttura grafica.

<details>
<summary>xaml</summary>

```xaml
<Grid Grid.Row="1" Height="50" VerticalAlignment="Bottom">
    <Grid.RowDefinitions>
        <RowDefinition></RowDefinition>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
    </Grid.ColumnDefinitions>

    <StackPanel Grid.Column="3" VerticalAlignment="Center" HorizontalAlignment="Center" >
        <TextBlock x:Name="txtDataCo" Text="Data" Grid.Column="2"
                HorizontalAlignment="Center" VerticalAlignment="Center"
                    FontWeight="Bold"
                ></TextBlock>
        <TextBlock x:Name="txtOraCo" Text="Ora" Grid.Column="2"
                HorizontalAlignment="Center" VerticalAlignment="Center"
                ></TextBlock>
    </StackPanel>
</Grid>
```
Anche per il footer stesso sarà nececssario creare una grigla, di modo da poter posizionare data e ora in basso a destra. Per fare ciò definiamo una singola riga e ben 4 colonne, per poi posizionare uno <b><i><StackPanel></i></b> nella quarta colonna (<b><i>Grid.Column="3"</i></b>), ci assicuriamo di allineare a dovere il contenuto che andrà inserito nel nostro <b><i><StackPanel></i></b> tramite <b><i>VerticalAlignment="Center"</i></b> e <b><i>HorizontalAlignment="Center"</i></b>. Successivamente inseriamo all'interno dello <b><i><StackPanel></i></b> 2 <b><i><TextBlock></i></b> all'interno dei quali andremo ad inserire data e ora, che verranno gestiti e calcolati tramite c# all'interno del nostro file <b>MainWindow.xaml.cs</b>, proprio per questo motivo è necessario assegnare un nome ad entrambi i <b><i><TextBlock></i></b> usando <b><i>x:Name="nomeComponente"</i></b>, di modo da poter essere richiamati e modificati tramite c#. Per concludere assegnamo un testo di default ad tramite <b><i>Text="testoDiDefault"</i></b>.
</details>

<details>
<summary>c#</summary>

```c#
public partial class MainWindow : Window
{
        private Timer _timer;
```

Partiamo col definire un oggetto di classe <b><i>Timer</i></b> all'interno di <b><i>MainWindow : Window</i></b>, questa classe ci permetterà di aggiornare <b><i>_timer</i></b> dopo un intervallo di tempo da noi definito.

```c#
public MainWindow()
{
        _timer = new Timer(Stufa, null, 0, 1000);
}
```

Proseguiamo con l'assegnazione di <b><i>_timer</i></b> all'interno di <b><i>MainWindow()</i></b>, definendo dei parametri ovvero <b><i>Stufa</i></b>: la funzione che verrà chiamata, <b><i>null</i></b>: parametro che verrà passato alla funzione, <b><i>0</i></b>: tempo (in millisecondi) aspettato prima del primo scatto (prima volta che aggiornerà i suoi valori) e infine <b><i>1000</i></b>: tempo (in millisecondi) aspettato prima del prossimo scatto (prossima volta che aggiornerà i suoi valori), in questo modo ogni secondo il timer verrà aggiornato, in caso non si desideri mostrare anche i secondi, basterà cambiare il valore in <b><i>60000</i></b> (millisecondi in un minuto).

```c#
private void Stufa(object state)
{
    Dispatcher.Invoke(() =>
    {

        txtDataCo.Text = DateTime.Now.ToString().Split(' ')[0];
        txtOraCo.Text = DateTime.Now.ToString().Split(' ')[1];
    });
}
```

L'ultima cosa che rimane da fare è definire la nostra funzione <b><i>Stufa()</i></b>, all'interno della nostra funzione creiamo un <b><i>Dispatcher.Invoke</i></b>, il quale ci permetterà di "bucare" il processo <b><i>Timer</i></b>, per poter apportare modifiche all'interno di quello della GUI. All'interno del <b><i>Dispatcher.Invoke</i></b> inseriamo le istruzioni da eseguire : tramite i nome precedentemente assegnati ai <b><i><TextBlock></i></b>, andiamo a modificare il loro contenuto, utilizziando <b><i>DateTime.Now</i></b> otterremo data e ora attuale, le quali verranno convertite in stringa tramite <b><i>.ToString()</i></b>, successivamente le due verranno immagazziante all'interno di un array tramite lo <b><i>Split(' ')</i></b>, inserendo poi un <b><i>[0]</i></b> o <b><i>[1]</i></b> andremo ad ottenere rispettivamente la data e l'ora attuali.
</details>

## Tab

<details>
<summary>Bubble (sort)</summary>

<details>
<summary>xaml</summary>
</details>

<details>
<summary>c#</summary>
</details>

</details>



<details>
<summary>Collatz</summary>

<details>
<summary>xaml</summary>
</details>

<details>
<summary>c#</summary>
</details>

</details>



<details>
<summary>Telefono</summary>

<details>
<summary>xaml</summary>
</details>

<details>
<summary>c#</summary>
</details>

</details>



<details>
<summary>Acronimo</summary>

<details>
<summary>xaml</summary>
</details>

<details>
<summary>c#</summary>
</details>

</details>



<details>
<summary>Isogramma</summary>

<details>
<summary>xaml</summary>
</details>

<details>
<summary>c#</summary>
</details>

</details>
