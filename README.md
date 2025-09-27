# 🎶 MixMashter

MixMashter est une application web complète permettant de créer, gérer et écouter des **mashups musicaux**.  
Le projet est divisé en deux parties :

- **Backend** : API REST en ASP.NET Core (C#)  
- **Frontend** : application Blazor WebAssembly (C# + Razor Components)

NOTE ⏲️ La version actuelle est en cours de développement et donc non candidate à une release ou déploiement , notamment pour des soucis au niveau dui FrontEnd

- 29.09.2025 : Remise du projet en état intermédiaire , le projet continuera en dehors du cadre scolaire

---

## 🚀 Installation et clonage

### Cloner le projet
```bash
# Récupération du dépôt
git clone https://github.com/<ton-compte-github>/MixMashter.git

# Aller dans le dossier
cd MixMashter
```
Backend (API)
```bash
cd MixMashter.API

# Restaurer les dépendances
dotnet restore

# Lancer en HTTPS (port par défaut 7141)
dotnet run
```

Frontend (Blazor)
```bash
cd MixMashter.Blazor

# Restaurer les dépendances
dotnet restore

# Lancer en mode développement
dotnet run
```
Le frontend sera accessible sur :
👉 https://localhost:7276

🛠️ Fonctionnalités
🎤 Utilisateurs & Authentification

Inscription & connexion (JWT)

Gestion des rôles : User, Masher, Admin

Changement de mot de passe

Navigation dynamique (boutons visibles selon rôle)

🎶 Mashups

Création de mashups (titre, description, format, cover, audio)

Upload de fichiers (cover .jpg/.png et audio .mp3/.wav)

Lecture via <audio> (fichiers locaux) ou intégration YouTube

Listing avec affichage cover + lecteur audio intégré

🎼 Playlists

Création de playlists

Ajout/suppression de mashups

Affichage du contenu d’une playlist

(Optionnel) partage/public/privé

🎤 Artistes & Chansons

Gestion des artistes

Gestion des chansons

Recherche et associations (mashups → chansons/artistes)

⭐ Favoris (prévu)

Ajout/retrait de mashups en favoris

Consultation rapide de ses favoris

📐 Architecture

Backend (ASP.NET Core Web API)

Couches : Controller → Services (BLL) → Repository (DAL)

DTOs pour communication front/back

Authentification par JWT

Swagger pour documentation

Frontend (Blazor WebAssembly)

Composants Razor = UI

Services Blazor (HttpClient) = ViewModels (MVVM simplifié)

API consommée via services (PlaylistApiService, MashupApiService, etc.)

Auth géré avec AuthenticationStateProvider + JwtAuthStateProvider

📸 Aperçu de l’UI

Login / Register

Navbar dynamique (Login, Logout, Playlists, Ajout Mashup…)

Listing de mashups avec cover et lecteur intégré

Formulaire d’ajout (upload fichiers + description)

📖 Documentation API

La documentation complète des endpoints est disponible via Swagger :
👉 https://localhost:7141/swagger

Exemples de routes :

POST /api/Auth/login → connexion (JWT)

POST /api/Mashup → créer un mashup

GET /api/Mashup → lister les mashups

POST /api/Mashup/upload → upload fichier cover/audio

🤝 Contribution

Fork le repo

Crée une branche (git checkout -b feature/ma-fonctionnalite)

Commit (git commit -m "Ajout nouvelle fonctionnalité")

Push (git push origin feature/ma-fonctionnalite)

Crée une Pull Request 🚀

📜 Licence

Projet académique — usage pédagogique uniquement.