using System;
using System.Collections.Generic;

class Facturation
{
    private List<Client> clients;
    private List<Produit> produits;
    private List<Facture> factures;

    public Facturation()
    {
        clients = new List<Client>();
        produits = new List<Produit>();
        factures = new List<Facture>();
    }

    public void AjouterClient(Client client)
    {
        clients.Add(client);
    }

    public Client RechercherClient(string nom)
    {
        return clients.Find(client => client.Nom.Equals(nom));
    }

    public void MettreAJourClient(Client client)
    {
      
        Client clientExistant = RechercherClient(client.Nom);

        if (clientExistant != null)
        {
         
            clientExistant.Adresse = client.Adresse;
            clientExistant.Telephone = client.Telephone;
        }
    }

    public void SupprimerClient(Client client)
    {
        clients.Remove(client);
    }

    public void AjouterProduit(Produit produit)
    {
        produits.Add(produit);
    }

    public Produit RechercherProduit(string nom)
    {
        return produits.Find(produit => produit.Nom.Equals(nom));
    }

    public void MettreAJourProduit(Produit produit)
    {
     
        Produit produitExistant = RechercherProduit(produit.Nom);

        if (produitExistant != null)
        {
         
            produitExistant.Description = produit.Description;
            produitExistant.Prix = produit.Prix;
        }
    }

    public void SupprimerProduit(Produit produit)
    {
        produits.Remove(produit);
    }

    public Facture CreerFacture(Client client)
    {
        Facture facture = new Facture(client);
        factures.Add(facture);
        return facture;
    }

    public void AjouterProduitFacture(Facture facture, Produit produit, int quantite)
    {
        facture.AjouterProduit(produit, quantite);
    }

    public decimal CalculerMontantTotal(Facture facture)
    {
        return facture.CalculerMontantTotal();
    }

    public void AfficherFacture(Facture facture)
    {
        facture.Afficher();
    }
}

class Client
{
    public int Id { get; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public string Telephone { get; set; }

    private static int count = 0;

    public Client(string nom, string adresse, string telephone)
    {
        Id = ++count;
        Nom = nom;
        Adresse = adresse;
        Telephone = telephone;
    }
}

class Produit
{
    public int Id { get; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public decimal Prix { get; set; }

    private static int count = 0;

    public Produit(string nom, string description, decimal prix)
    {
        Id = ++count;
        Nom = nom;
        Description = description;
        Prix = prix;
    }
}

class Facture
{
    public int Id { get; }
    public Client Client { get; }
    public List<ProduitFacture> Produits { get; }
    public DateTime DateCreation { get; }

    private static int count = 0;

    public Facture(Client client)
    {
        Id = ++count;
        Client = client;
        Produits = new List<ProduitFacture>();
        DateCreation = DateTime.Now;
    }

    public void AjouterProduit(Produit produit, int quantite)
    {
        ProduitFacture produitFacture = new ProduitFacture(produit, quantite);
        Produits.Add(produitFacture);
    }

    public decimal CalculerMontantTotal()
    {
        decimal total = 0;
        foreach (ProduitFacture produitFacture in Produits)
        {
            total += produitFacture.Produit.Prix * produitFacture.Quantite;
        }
        return total;
    }

    public void Afficher()
    {
        Console.WriteLine("Facture n°" + Id);
        Console.WriteLine("Client : " + Client.Nom);
        Console.WriteLine("Date de création : " + DateCreation);
        Console.WriteLine("Produits :");
        foreach (ProduitFacture produitFacture in Produits)
        {
            Console.WriteLine("- " + produitFacture.Produit.Nom + " (Quantité : " + produitFacture.Quantite + ")");
        }
        Console.WriteLine("Montant total : " + CalculerMontantTotal());
    }
}

class ProduitFacture
{
    public Produit Produit { get; }
    public int Quantite { get; }

    public ProduitFacture(Produit produit, int quantite)
    {
        Produit = produit;
        Quantite = quantite;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Facturation facturation = new Facturation();

        
        Client client1 = new Client("Atana Essowazam N.", " Ouest-Foire", "777894563");
        facturation.AjouterClient(client1);

        
        Produit produit1 = new Produit("écouteur bluetooth", "écouteur avec 18h d'autonomie, pratique pour les longs trajets", 32000);
        facturation.AjouterProduit(produit1);

        
        Facture facture1 = facturation.CreerFacture(client1);

        
        facturation.AjouterProduitFacture(facture1, produit1, 3);

        
        facturation.AfficherFacture(facture1);
    }
}
