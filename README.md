# WPFTabs

## Descrizione
#### Esercizio WPF in C# da 5 tab diverse (Bubble, Collatz, Telefono, Acronimo, Isogramma). Ogni tab avrà una sua funzione che verrà eseguita indipendentemente (anche a tab non aperta) e disporrà inoltre di una descrizione della propria funzione e di un footer contenente data e ora.



## Applicazione
Prima di tutto bisognerà regolare la grandezza desiderata per la nostra applicazione e fare in modo che, una volta avviata, si posizioni al centro dello schermo dell'utente.
```xaml
<Window x:Class="MartinezBianchi.Michelle._3i.wpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MartinezBianchi.Michelle._3i.wpf"
        mc:Ignorable="d"
        
        Title="MartinezBianchi.Michelle.3i.wpf" 
        Height="600" 
        Width="600" 
        WindowStartupLocation="CenterScreen"
        >
```

Ciò sarà possibile andando a modificare <b><i>Height</i></b> e <b><i>Width</i></b> (in caso quelli impostati automaticamente non ci soddisfino). Per il posizionamento dell'applicazione basterà invece modificare l'attributo <b><i>WindowStartupLocation</i></b>. In aggiunta sarà possibile cambiare il titolo della propria app tramite l'attributo <b><i>Title</i></b>.
<br>
## Griglia
Sarà poi necessario creare una griglia per contenere tab e footer, di modo da non dover creare un footer identico per ogni tab e poter riutilizzare sempre lo stesso sempliccemente posizionandolo in basso.
        
```xaml
<Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition Height="50"></RowDefinition>
        </Grid.RowDefinitions>
```

Il primo tag <b><i><</i></b><b><i>Grid</i></b><b><i>></i></b> andrà chiuso una volta sviluppata la partre grafica di ogni singola tab, dato che sarà la griglia che conterrà l'intera applicazione. Delle due righe all'interno di <b><i><Grid.RowDefinitions></i></b> la prima andrà a contenere le diverse tab e la seconda il footer, proprio per questo essa verrà ridimensionata, di modo da non fargli occupare troppo spazio. 


## Footer
Dopo aver creato la prima griglia, il prossimo componente da creare sarà il footer, comune per tutte le diverse tab.

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
Anche per il footer stesso sarà nececssario creare una griglia, di modo da poter posizionare data e ora in basso a destra. Per fare ciò definiamo una singola riga e ben 4 colonne, per poi posizionare uno <<b><i>StackPanel</i></b>> nella quarta colonna (<b><i>Grid.Column="3"</i></b>). Assicuriamoci poi di allineare a dovere il suo contenuto utilizzando <b><i>VerticalAlignment="Center"</i></b> e <b><i>HorizontalAlignment="Center"</i></b>. Successivamente posizioniamo al suo interno 2 <<b><i>TextBlock</i></b>> nei quali andremo ad inserire data e ora, che verranno gestiti e calcolati tramite c# all'interno del nostro file <b>MainWindow.xaml.cs</b>, proprio per questo motivo è necessario assegnare un nome ad entrambi i <<b><i>TextBlock</i></b>> usando <b><i>x:Name="nomeComponente"</i></b>, di modo da poter essere richiamati e modificati.
</details>

<details>
<summary>c#</summary>

```c#
public partial class MainWindow : Window
{
        private Timer _timer;
```

Partiamo col definire un oggetto di tipo <b><i>Timer</i></b> all'interno di <b><i>MainWindow : Window</i></b>.
<br><br>

```c#
public MainWindow()
{
        InitializeComponent();
        _timer = new Timer(Stufa, null, 0, 1000);
}
```

Proseguiamo con l'inizializzazione di <b><i>_timer</i></b> all'interno di <b><i>MainWindow()</i></b> definendo dei parametri ovvero <b><i>Stufa()</i></b>: la funzione che verrà chiamata, <b><i>null</i></b>: parametro che verrà passato alla funzione, <b><i>0</i></b>: tempo (in millisecondi) aspettato prima del primo scatto (prima volta che verrà chiamata la funzione) e infine <b><i>1000</i></b>: tempo (in millisecondi) aspettato prima del prossimo scatto (prossima volta che verrà chiamata la funzione), in questo modo ogni secondo il timer verrà aggiornato tramite <b><i>Stufa()</i></b>.
<br><br>

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

L'ultima cosa che rimane da fare è definire la nostra funzione <b><i>Stufa()</i></b>. Al suo interno creiamo un <b><i>Dispatcher.Invoke</i></b>, il quale ci permetterà di "bucare" il processo <b><i>Timer</i></b> (processo corrente), per poter apportare modifiche all'interno di quello della GUI. All'interno del <b><i>Dispatcher.Invoke</i></b> inseriamo le istruzioni da eseguire : tramite i nomi precedentemente assegnati ai <<b><i>TextBlock</i></b>>, andiamo a modificare il loro contenuto.
</details>

## Tab
Per poter ottenere le diverse tab, sarà necessario creare un <<b><i>TabControl</i></b>> che le possa contenere, e posizionarlo nella prima righa della griglia contenente tutta l'applicazone.<br>
N.B. Il <<b><i>TabControl</i></b>> andrà chiuso solamente una volta inseriti tutti i diversi <<b><i>TabItem</i></b>>.

```xaml
<TabControl Grid.Row="0">
        <TabItem Header="Bubble"></TabItem>
        <TabItem Header="Collatz"></TabItem>
        <TabItem Header="Telefono"></TabItem>
        <TabItem Header="Acronimo"></TabItem>
        <TabItem Header="Isogramma"></TabItem>
</TabControl>
```


<details>
<summary>Bubble (sort)</summary>
Questa tab si occuperà di generare un vettore randomico con numeri che vanno da 1 a 100, per poi riordinarlo tramite bubble sort.
<br>        
<details>
<summary>xaml</summary>

```xaml
<TabItem Header="Bubble">
    <Grid>

        <Grid.RowDefinitions>
            <RowDefinition Height="70"></RowDefinition>
            <RowDefinition Height="100"></RowDefinition>
            <RowDefinition Height="20"></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>

        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>
```

Per prima cosa diamo un nome, che verrà mostrato all'utente, al nostro <<b><i>TabItem</i></b>> tramite l'attributo <b><i>Header</i></b>, successivamente creaiamo un griglia composta da 4 righe e 2 colonne ridimensionate in base ai loro scopi. Le righe serviranno per disporre al loro interno : descrizione, pulsante in grado di rigenerare un vettore e riordinarlo, tipo di sequenza sottostante (generata o ordinata) e la sequenza stessa. Le colonne saranno invece utili a tenere separate le due sequenza, ecco la rappresentazione grafica qui sotto.
<img src="https://github.com/MichelleMyBad/WPFTabs/assets/127590227/9182aba5-d81f-42d0-8d66-685d4e2b404b">
<br><br>

```xaml
<RichTextBox Grid.Row="0" IsReadOnly="True" Grid.ColumnSpan="2">
    <FlowDocument>
        <Paragraph>
            Generato un vettore randomico con numeri che vanno da 1 a 100, il programma sarà in grado di riordinarlo tramite bubble sort
        </Paragraph>
    </FlowDocument>
</RichTextBox>
```

Proseguiamo con l'inserire all'interno della prima riga una breve descrizione di ciò di cui si occuperà questa tab: creaiamo una <<b><i>RichTextBox</i></b>> ed aggiungiamo <b><i>Grid.ColumnSpan="2"</i></b> come attributo di modo che si espanda per entrambe le nostre colonne, un altro attributo necessario sarà <b><i>IsReadOnly="True"</i></b> il quale servirà ad impedire all'utente di modificare il testo. Al suo interno inseriamo poi un <<b><i>FlowDocument</i></b>> che ci permetterà di scrivere la nostra descrizione dentro un <<b><i>Pararaph</i></b>>. 
<br><br>

```xaml
<Button  Grid.Column="0" Grid.Row="1" Grid.ColumnSpan="2" 
            Click="Button_Click_2">Rigenera vettore</Button>
```

Proseguendo, nella seconda riga, troveremo il nostro pulsante incaricato del riordinamento e generazione dei vettori, ci basterà associargli una funzione che verrà chiamata al click, ecco qui sotto un metodo veloce per poterlo fare.

<details>
        <summary>Creazione della funzione chiamata al click</summary> 
        <img src="https://github.com/MichelleMyBad/WPFTabs/assets/127590227/8aaeef93-0382-4fdf-8eb0-98cd891cf58d"> <br>
        Per creare una funzione associata al click del pulsante in modo semplice e veloce basterà fare doppio click su <b><i>Button</i></b> in questo modo.<br><br>
        <img src="https://github.com/MichelleMyBad/WPFTabs/assets/127590227/aec673f7-3416-4124-9897-c9a0b581f051"> <br>
        Sarà poi necessario andare nelle <b>Properties</b> e accedere alla sezione <b>Event Handler</b> cliccando dove mostrato in immagine.<br><br>
        <img src="https://github.com/MichelleMyBad/WPFTabs/assets/127590227/3657050e-0cb0-4227-8536-34f2a7ef7933)"><br> 
        Fare poi doppio click nello spazio a fianco a <b>Click</b> e verrà generato automaticamente l'attributo <b><i>Click="NomeFunzione"</i></b> al bottone e una funzione omonima all'interno del file <b>MainWindow.xaml.cs</b>
</details>
<br>

```xaml
<RichTextBox Grid.Row="2" Grid.Column="0" IsReadOnly="True" Height="100">
<FlowDocument>
    <Paragraph>Sequenza generata</Paragraph>
</FlowDocument>
</RichTextBox>

<RichTextBox Grid.Row="2" Grid.Column="1" IsReadOnly="True" Height="100">
<FlowDocument>
    <Paragraph>Sequenza riordinata</Paragraph>
</FlowDocument>
</RichTextBox>
```

Nella terza riga ci occuperemo semplicemente di inserire due <<b><i>RichTextBox</i></b>> dove poter scrivere <i>"Sequenza generata"</i> e <i>"Sequenza riordinata"</i>, di modo da permettere all'utente di differenziarle più facilmente.
<br><br>

```xaml
        <ListView Grid.Row="3" Grid.Column="0" x:Name="LstUnsorted"></ListView>
        <ListView Grid.Row="3" Grid.Column="1" x:Name="LstSorted"></ListView>
    </Grid>
</TabItem>
```

Per finire nell'ultima riga inseriamo le due <<b><i>ListView</i></b>> all'interno delle quali andremo a mostrare a schermo i nostri vettori (generato e ordinato). Bisognerà inoltre dargli dei nomi, di modo da poterle andare a modificare tramite c#.
<br>
</details>

<details>
<summary>c#</summary>

```c#
public partial class MainWindow : Window
{
        int[] unsortedStatic = new int[] { 2, 1, 3, 8, 7, 5, 6, 4, 100, 79, 1, 2, 10 };
        int[] unsorted = new int[] { 2, 1, 3, 8, 7, 5, 6, 4, 100, 79, 1, 2, 10 };
        Thread thread1;
        Thread thread2;
```

Per prima cosa creiamo all'interno di <b><i>MainWindow : Window</i></b> due liste di numeri, rispettivamente una che resterà nella lista di numeri creati e una che verrà poi riordinata. Creiamo poi due <b><i>Thread</i></b> che si occuperanno rispettivamente di gestire lo smistamento (bubble sort) e di generare un nuovo vettore.
<br><br>

```c#
public MainWindow()
{
    InitializeComponent();
    thread1 = new Thread(Sort);
    thread1.Start();
    LstUnsorted.ItemsSource = unsortedStatic;
}
```

Successivamente avviamo la funzione <b><i>Sort()</i></b> nel <b><i>thread1</i></b> e assegniamo alla <<b><i>ListView</i></b>>, che mostrerà la sequenza di partenza, l'apposito array di numeri.
<br><br>

```c#
public void Sort()
{
    Dispatcher.Invoke(() =>
    {
        LstSorted.ItemsSource = unsorted;
    });
    int tmp;
    for (int lunghezza = unsorted.Length; lunghezza > 1; lunghezza--)
    {
        for (int i = 0; i < lunghezza - 1; i++)
        {
            if (unsorted[i] > unsorted[i + 1])
            {
                tmp = unsorted[i];
                unsorted[i] = unsorted[i + 1];
                unsorted[i + 1] = tmp;
            }
            Dispatcher.Invoke(() =>
            {
                LstSorted.Items.Refresh();
            });
            Thread.Sleep(100);
        }
    }
}
```
Nella fuinzione <b><i>Sort()</i></b> inzieremo subito con l'aggiornare la <<b><i>ListView</i></b>> che dovrà contenere la sequenza riordinata alla fine del bubble sort, assegnandole il vettore apposito. Utilizziamo poi un semplice bubble sort per occuparci del riordinamento e, al termine di ogni ciclo, aggiorniamo l'interfaccia grafica tramite <b><i>Dispatcher.Invoke</i></b>, mostrando così all'utente il riordinamento in modo progressivo.
<br><br>

```c#
private void Button_Click_2(object sender, RoutedEventArgs e)
{
    if (thread1.ThreadState == ThreadState.Stopped)
    {
        if (thread2 == null || thread2.ThreadState == ThreadState.Stopped)
        {
            thread2 = new Thread(Generate);
            thread2.Start();
        }

    }
}
```
Proseguiamo con l'iniziare la generazione e il riordinamento di un nuovo vettore al click del pulsante: per prima cosa controlliamo che entrambi i <b><i>Thread</i></b> si siano conclusi o, in caso del <b><i>thread2</i></b>, inizializzati. In caso si verifichino queste condizioni, inizializiamo il <b><i>thread2</i></b> di modo che esegua la funzione <b><i>Generate()</i></b>.
<br><br>

```c#
public void Generate()
{
    var random = new Random();
    for (int x = 0; x < unsorted.Length; x++)
    {
        unsortedStatic[x] = random.Next(1, 101);
        unsorted[x] = unsortedStatic[x];
        Dispatcher.Invoke(() =>
        {
            LstUnsorted.Items.Refresh();
            LstSorted.Items.Refresh();
        });
        Thread.Sleep(100);
    }
    thread1 = new Thread(Sort);
    thread1.Start();
}
```

La funzione <b><i>Generate()</i></b>, tramite ciclo <i>for</i>, si occuperà di aggiornare le liste con una nuova sequenza di numeri casuali (da 1 a 100). Ad ogni passo del ciclo <i>for</i> andremo a fare refresh delle <<b><i>ListView</i></b>> tramite <b><i>Dispatcher.Invoke</i></b>, di modo da mostrare all'utente il progredire della rigenerazione dei numeri casuali. Infine avviamo nuovamente nel <b><i>thread1</i></b> la funzione <b><i>Sort()</i></b>.
        
</details>
<br>
</details>



<details>
<summary>Collatz</summary>
Questa tab si occuperà di, una volta passato un numero al programma, ritornare il numero di passi necessare a raggiungere uno seguendo la <a href="https://it.wikipedia.org/wiki/Congettura_di_Collatz" target="_blank">congettura matematica di Lothar Collatz</a>. 
<br>
<details>
<summary>xaml</summary>

```xaml
<TabItem Header="Collatz">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
        </Grid.ColumnDefinitions>
```

Come visto per la tab precedente, cominciamo col creare una griglia per questa tab e col dargli un nome visibile a schermo.
<br><br>

```xaml
<RichTextBox IsReadOnly="True">
    <FlowDocument>
        <Paragraph>
            Tra i problemi irrisolti in matematica c’è una congettura (una affermazione che nei fatti sembra vera ma di cui nessuno riesce a dimostrarne la falsità) espressa per la prima volta nel 1937 da Lothar Collatz nella quale si afferma questo:
            <LineBreak/>
            - Prendi un numero intero positivo n.
            <LineBreak/>
            - Se n è pari, dividilo per 2.
            <LineBreak/>
            - Se n è dispari, moltiplicalo per 3 e aggiungigli 1 per ottenere 3n + 1.
            <LineBreak/>
            - Usa questo risultato ripetendo il processo all’infinito.
            <LineBreak/>
            - Indipendentemente dal numero con cui inizi, alla fine raggiungerai sempre 1.
            <LineBreak/>
            - Quello da stabilire qui è quanti passi servono per farlo!!
            <LineBreak/>
            <LineBreak/>
            A proposito di questa congettura, il famoso matematico ungherese Paul Erdős disse: *«La matematica non è ancora pronta per problemi di questo tipo»* ed offrì 500 dollari per la sua soluzione[1].
            <LineBreak/>
            <LineBreak/>
            Questo esercizio prevede di realizzare un programma che passato un intero N, torni il numero di passi necessari a raggiungere 1.
            <LineBreak/>
            <LineBreak/>
            Riferimenti storici
            https://it.wikipedia.org/wiki/Congettura_di_Collatz
        </Paragraph>
    </FlowDocument>
</RichTextBox>
```

Continuiamo con una <b><i><RichTextBox></i></b> nella quale poter inserire la traccia dell'esercizio.
<br><br>

```xaml
         <StackPanel Grid.Row="1">
             <Button Click="Button_Click_1">Calcola passi</Button>
             <TextBox x:Name="edtCollatz" HorizontalContentAlignment="Center">Inserire numero</TextBox>
             <TextBlock>Numero di passi : </TextBlock>
             <TextBlock x:Name="txtCollatz"></TextBlock>
         </StackPanel>
     </Grid>
</TabItem>
```

Concludiamo la parte grafica creando uno <b><i><StackPanel></i></b> nel quale poter inserire un bottone, che si occuperà di chiamare la funzione per eseguire il calcolo, un <b><i><TextBlock></i></b> per permettere all'utente di inserire il numero e due <b><i><TextBlock></i></b> per poter mostrare a schermo il risultato ottenuto.
<br>
</details>

<details>
<summary>c#</summary>

```c#
public int Passi(int n)
{
    int counter = 0;
    if (n <= 0)
    {
        throw new ArgumentException();
    }

    for (counter = 0; n > 1; counter++)
    {
        if (n % 2 == 0)
        {
            n = n / 2;
        }
        else if (n % 2 == 1)
        {
            n = (n * 3 + 1);
        }
    }
    return counter;
}
```

Iniziamo col definire la funzione che si occuperà di eseguire il calcolo, per prima cosa dobbiamo assicurarci che il numero di maggiore di 0, per poi iniziare a seguire passo passo le istruzioni dateci dalla <a href="https://it.wikipedia.org/wiki/Congettura_di_Collatz" target="_blank">congettura di Collatz</a>, per poi fare il retunr del numero di passi eseguiti.
<br><br>

```c#
private void Button_Click_1(object sender, RoutedEventArgs e)
{
    try
    {
        int valore = Convert.ToInt32(edtCollatz.Text);
        int risultato = Passi(valore);
        txtCollatz.Text= risultato.ToString();
    }
    catch(Exception) 
    {
        MessageBox.Show("Inserire un numero maggiore di 0 nella casella");
    }
    catch (Exception)
    {
        MessageBox.Show("Inserire un numero nella casella");
    }
}
```

Al click del pulsante faremo invece in modo di ricavare il numero inserito dall'utente, mettere in <b><i>risultato</i></b> il numero di passi eseguiti, ottenuto tramite la funzione <b><i>Passi</i></b>, ed infine inseriremo questo risultato all'interno del <b><i><TextBlock></i></b> creato in precedenza. Per prevenire eventuali errori durante l'esecuzione, metteremo il tutto all'interno di un <i>try-catch</i>, in modo che, in caso di input indesiderato, l'applicazione faccia notare all'utente il suo errore tramite <b><i>MessageBox.Show</i></b>.   
</details>
<br>
</details>


<details>
<summary>Telefono</summary>
Questa tab si occuperà di, in caso si tratti di un numero valido, pulire la stringa inserita dall'utente perchè sia valida ai fini del NAMP americano.
<br>
<details>
<summary>xaml</summary>

```xaml
<TabItem Header="Telefono">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>
```

Partiamo come al solito col creare griglia e titolo per la nostra tab.
<br><br>

```xaml
<RichTextBox IsReadOnly="True" Grid.ColumnSpan="2">
    <FlowDocument>
        <Paragraph>
            Pulire la stringa in ingresso perchè sia valida ai fini del NAMP Americano.
            <LineBreak/>
            <LineBreak/>
            Il North American Numbering Plan (NANP) è un sistema di numerazione telefonica utilizzato da molti paesi del Nord America come gli Stati Uniti, il Canada o le Bermuda.
            <LineBreak/>
            <LineBreak/>
            Tutti i paesi NANP condividono lo stesso prefisso internazionale: 1.
            <LineBreak/>
            <LineBreak/>
            I numeri NANP sono numeri di dieci cifre costituiti da
            <LineBreak/>
            - un codice di area a tre cifre, comunemente noto come prefisso,
            <LineBreak/>
            - seguito da un numero locale di sette cifre.
            <LineBreak/>
            - Le prime tre cifre del numero locale rappresentano il codice di scambio,
            <LineBreak/>
            - seguito dal numero univoco di quattro cifre *(che è il numero dell'abbonato)*.
            <LineBreak/>
            <LineBreak/>
            Il formato è generalmente rappresentato come
            <LineBreak/>
            <LineBreak/>
            (NXX) -NXX-XXXX
            <LineBreak/>
            <LineBreak/>
            dove
            <LineBreak/>
            - N è qualsiasi cifra compresa tra 2 e 9
            <LineBreak/>
            - X è qualsiasi cifra compresa tra 0 e 9
        </Paragraph>
    </FlowDocument>
</RichTextBox>
```

Continuiamo con la solita <b><i><RichTextBox></i></b> per poter inserire la descrizione di ciò che la tab andrà a svolgere.
<br><br>

```xaml
                <StackPanel Grid.Row="1" Grid.ColumnSpan="2">
                        <Button Click="Button_Click_3" Grid.ColumnSpan="2">Pulisci numero</Button>
                        <TextBox HorizontalContentAlignment="Center" x:Name="edtTelefono" Grid.ColumnSpan="2">Inserire numero</TextBox>
                        <!--colonna 0-->
                        <TextBlock Grid.Column="0">Ultimo numero pulito :</TextBlock>
                        <TextBlock x:Name="txtTelefono" Grid.Column="0"></TextBlock>
                        <!--colonna 1-->
                        <TextBlock Grid.Column="1">Ultima stringa inserita :</TextBlock>
                        <TextBlock x:Name="txtstrTelefono" Grid.Column="1"></TextBlock>
                </StackPanel>
        </Grid>
</TabItem>
```

Per poi concludere con uno <b><i><StackPanel></i></b> contenente pulsante, <b><i><TextBox></i></b> per l'input e <b><i><TextBlock></i></b> per mostrare l'ultima stringa inserita e il numero pulito all'utente.
<br>
</details>

<details>
<summary>c#</summary>

```c#
public string Pulisci(string phoneNumber)
{
    for (int i = 0; i < phoneNumber.Length; i++)
    {

        if (char.IsDigit(phoneNumber[i]) == false)
        {
            phoneNumber = phoneNumber.Remove(i, 1);
            i--;
        }
    }
    if (phoneNumber[0] == '1')
    {
        phoneNumber = phoneNumber.Remove(0, 1);
    }
    if (phoneNumber.Length != 10)
    {
        throw new ArgumentException();
    }
    else if (Convert.ToInt32(phoneNumber[0].ToString()) < 2 || Convert.ToInt32(phoneNumber[3].ToString()) < 2)
    {
        throw new ArgumentException();
    }
    else
    {
        return phoneNumber;
    }

}
```

Per cominciare creaiamo una funzione che si in grado di ripulire il nostro numero secondo gli standard del NANP americano e che poi ritorni il numero ripulito o un errore in caso di input errato.
<br><br>

```c#
private void Button_Click_3(object sender, RoutedEventArgs e)
{
    try
    {
        string numero = edtTelefono.Text;
        txtstrTelefono.Text = numero;
        string numeroPulito = Pulisci(numero);
        txtTelefono.Text = numeroPulito;
    }
    catch(Exception)
    {
        MessageBox.Show("Inserire un numero valido nella casella");
    }
}
```
Continuiamo poi con l'immagazzinare la strinfa inserita all'interno di <b><i>numero</i></b>, per poi fornire questo dato alla <b><i><TextBox></i></b>, che si occupa di mostrare a schermo l'ultima stringa inserita, e a <b><i>Pulisci</i></b>, riusltato della quale verrà immagazzinato all'interno della <b><i><TextBox></i></b> in modo da mostrarlo a schermo. utilizziamo nuovamente un <i>try-catch</i> per prevenire il crash dell'applicazione in caso di errori.     
</details>
<br>
</details>


<details>
<summary>Acronimo</summary>
Questa tab si occuperà di, una volta inserita una serie di parole, ricavarlne l'acronimo.
<br>       
<details>
<summary>xaml</summary>

```xaml
<TabItem Header="Acronimo">
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="50"></RowDefinition>
                <RowDefinition></RowDefinition>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition></ColumnDefinition>
            </Grid.ColumnDefinitions>
```

Creiamo come al solito la nostra griglia e titolo per la tab.
<br><br>

```xaml
<RichTextBox IsReadOnly="True" Grid.ColumnSpan="2">
        <FlowDocument>
            <Paragraph>
                Dato un insieme di parole al programma esso sarà in grado di crearne l'acronimo
            </Paragraph>
        </FlowDocument>
</RichTextBox>
```

Per poi proseguire con la solita descrizione.
<br><br>

```xaml
<StackPanel Grid.Row="1" Grid.ColumnSpan="2">
        <Button  Grid.ColumnSpan="2" Click="Button_Click">Ottieni acronimo</Button>
        <TextBox HorizontalContentAlignment="Center" x:Name="edtAcronimo" Grid.ColumnSpan="2">Inserire acronimo</TextBox>
        <TextBlock>Acronimo : </TextBlock>
        <TextBlock x:Name="txtAcronimo"></TextBlock>
</StackPanel>
```

Concludiamo in fine con <b><i><StackPanel></i></b> contenente bottone, <b><i><TextBlock></i></b> e <b><i><TextBlock></i></b> per permettere all'utente di inserire l'input e visualizzare l'acronimo.
<br>
</details>

<details>
<summary>c#</summary>

```c#
public string Abbrevia(string phrase)
{
    string acronimo = "";

    if (char.IsLetter(phrase[0]))
    {
        acronimo = phrase[0].ToString().ToUpper();
    }
    while (char.IsLetter(phrase[phrase.Length - 1]) == false) 
    {
        phrase=phrase.Substring(0, phrase.Length - 1);
    }
    int lunghezza = phrase.Length;
    for (int i = 0; i < lunghezza; i++)
    {
        if (phrase[i].ToString() == "'" && (phrase[i + 1].ToString() != "s" && phrase[i + 1].ToString() != "S"))
        {
            if (char.IsLetter(phrase[i + 1]))
            {
                acronimo = acronimo + phrase[i + 1].ToString().ToUpper();
            }

        }
        else if(char.IsLetter(phrase[i]) == false && phrase[i].ToString() != "'")
        {
            if (char.IsLetter(phrase[i + 1]))
            {
                acronimo = acronimo + phrase[i + 1].ToString().ToUpper();
            }
        }
    }
    return acronimo;
}
```

Creiamo una funzione in grado di prendere la prima lettera per nuova parola, riconoscendo se si trovi dopo uno spazio o altri tipi di punteggiatura. per poi ritornare l'acronimo ricavato. <br>
N.B. Bisognerà ricordarsi di specificare al programma di ignorare i possibili genitivi sassoni.
<br><br>

```c#
private void Button_Click(object sender, RoutedEventArgs e)
{
    try
    {
        string frase = edtAcronimo.Text;
        string acronimo = Abbrevia(frase);
        txtAcronimo.Text = acronimo;
    }
    catch(Exception)
    {
        MessageBox.Show("Inserire delle parole nella casella");
    }

}
```

Concludiamo con il solito <i>try-catch</i> nel quale ic occuperemo di prendere l'input dell'utente, passarlo alla funzione, mostrare l'acronimo ottenuto e avvisare l'utente in caso di errore.
</details>
<br>
</details>


<details>
<summary>Isogramma</summary>
Questa tab si occuperà di controllare se una parola o frase inserita è un isogramma (non presenta lettere ripetute).
<br>        
<details>
<summary>xaml</summary>

```xaml
<TabItem Header="Isogramma">
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition></RowDefinition>
                <RowDefinition></RowDefinition>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition></ColumnDefinition>
            </Grid.ColumnDefinitions>
```

Iniziamo con il creare nuovamente un titolo per quest'ultima tab e una griglia apposita. <br><br>

```xaml
<RichTextBox IsReadOnly="True" Grid.ColumnSpan="2">
        <FlowDocument>
            <Paragraph>
                Determina se una parola o una frase è un isogramma.
                <LineBreak/>
                <LineBreak/>
                Per come lo intendiamo in questo esercizio, un isogramma è una parola o una frase che non ha lettere ripetute.
                <LineBreak/>
                Sono ammessi spazi e segni di punteggiatura ripetuti.
                <LineBreak/>
                <LineBreak/>
                Esempi di isogrammi:
                <LineBreak/>
                <LineBreak/>
                - lumberjacks
                <LineBreak/>
                - background
                <LineBreak/>
                - downstream
                <LineBreak/>
                - six-year-old
                <LineBreak/>
                <LineBreak/>
                Gli isogrammi possono essere utili come chiavi di cifratura dato che la corrispondenza fra lettere è univoca.
                <LineBreak/>
                <LineBreak/>
                Isogrammi di 10 lettere, per esempio PATHFINDER, DUMBWAITER o BLACKHORSE, possono essere utilizzate da venditori di beni il cui prezzo può essere negoziato, come macchine usate, gioielli e antichità.
                <LineBreak/>
                <LineBreak/>
                Per esempio le cifre decimali possono essere mappate secondo questo schema:
                <LineBreak/>
                <LineBreak/>
                P	A	T	H	F	I	N	D	E	R
                <LineBreak/>
                <LineBreak/>
                1	2	3	4	5	6	7	8	9	0
        
            </Paragraph>
        </FlowDocument>
</RichTextBox>
```

Continuiamo con la <b><i><RichTextBox></i></b> per la descrizione.<br><br>


```xaml
            <StackPanel Grid.Row="1" Grid.ColumnSpan="2">
                <Button  Grid.ColumnSpan="2" Click="Button_Click_4">Controlla</Button>
                <TextBox HorizontalContentAlignment="Center" Grid.ColumnSpan="2" x:Name="edtIsogramma">Inserire parola</TextBox>
                <TextBlock x:Name="txtIsogramma"></TextBlock>
            </StackPanel>
        </Grid>
</TabItem>
```

Concludiamo in fine con uno <b><i><StackPanel></i></b> con al suo interno pulsante, <b><i><TextBox></i></b> per l'input e <b><i><TextBlock></i></b> per dire all'utente se la parola inserita è un isogramma o meno.
<br>
</details>

<details>
<summary>c#</summary>

```c#
public bool Verifica(string word)
{
    for (int i = 0; i < word.Length - 1; i++)
    {
        for (int j = i + 1; j < word.Length; j++)
        {
            if (Char.IsSymbol(word[i])==false && Char.ToLower(word[i]) == Char.ToLower(word[j]))
            {
                return false;
            }
        }
    }
    return true;
}
```

Andiamo a creare una funzione in grado di poter verificare se la parola o frase proposta presentino o meno lettere ripetute e che, in caso contrario, ritorni <i>true</i>, in quanto la parola o frase passata è risulterà essere un isogramma.
N.B. Sono permesse ripetizioni di caratteri diversi da lettere.
<br><br>

```c#
private void Button_Click_4(object sender, RoutedEventArgs e)
{
    string testo = edtIsogramma.Text;
    bool verificatore = Verifica(testo);

    if (verificatore == false)
    {
        txtIsogramma.Text = "La stringa inserita non è un isogramma";
    }
    else
    {
        txtIsogramma.Text = "La stringa inserita è un isogramma";
    }
}
```

Concludiamo con l'immagazzinare l'input dato dall'utente in una variabile, passarla alla funzione <b><i>Verifica</i></b> e poi mostrare all'utente se la parola o frase inserita sia un isogramma o meno.

</details>

</details>
