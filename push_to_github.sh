#!/usr/bin/env bash
set -euo pipefail

# Automated helper to add remote and push current repo to GitHub.
# WARNING: This script will create a commit with all current changes if none exist.
# It will prompt if the remote differs. Run from your repository root.

DEFAULT_REMOTE="https://github.com/VijayThangavel56/SimpleApplicationCrud.git"

echo "This script will push the current repository to a remote GitHub repo."
read -rp "Use default remote ($DEFAULT_REMOTE)? [Y/n]: " use_default

if [[ "$use_default" =~ ^(n|N)$ ]]; then
  read -rp "Enter remote URL (HTTPS or SSH): " REMOTE_URL
else
  REMOTE_URL="$DEFAULT_REMOTE"
fi

# Initialize git if necessary
if [ ! -d .git ]; then
  echo "Initializing git repository..."
  git init
fi

# Stage all changes
echo "Staging changes..."
git add -A

# Commit if there are staged changes
if ! git diff --cached --quiet; then
  read -rp "Enter commit message [Default: 'chore: deploy']: " commit_msg
  commit_msg=${commit_msg:-"chore: deploy"}
  git commit -m "$commit_msg"
else
  echo "No changes to commit."
fi

# Ensure main branch
git branch -M main || true

# Add remote (replace if exists)
if git remote get-url origin >/dev/null 2>&1; then
  echo "Remote 'origin' exists. Replacing with $REMOTE_URL"
  git remote remove origin
fi

git remote add origin "$REMOTE_URL"

# Push and set upstream (will prompt for credentials if needed)
echo "Pushing to $REMOTE_URL ..."

git push -u origin main

echo "Push complete. If authentication failed, configure git credentials (PAT for HTTPS or add SSH key)."
