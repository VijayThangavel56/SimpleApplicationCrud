Run these steps locally to push your code to GitHub.

1) Make the script executable and run it from the project root:

   chmod +x push_to_github.sh
   ./push_to_github.sh

2) Authentication
- For HTTPS, enter your GitHub username and a Personal Access Token (PAT) when prompted by git, if you have 2FA.
- For SSH, use the SSH remote (git@github.com:VijayThangavel56/SimpleApplicationCrud.git) and ensure your SSH key is added to GitHub.

3) Alternative manual commands

   git init
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git remote add origin https://github.com/VijayThangavel56/SimpleApplicationCrud.git
   git push -u origin main

4) If push is rejected because remote has commits already, fetch and rebase:

   git fetch origin
   git pull --rebase origin main
   git push origin main

Notes
- I cannot perform the remote push from this environment. Run the script above locally.
- If you want, I can generate a GitHub Actions workflow to auto-push or deploy, but that still requires repository access and secrets.
