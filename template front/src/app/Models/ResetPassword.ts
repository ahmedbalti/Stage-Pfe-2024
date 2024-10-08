export interface ResetPassword {
  token: string;
  email: string;
  password: string;
  confirmPassword: string; // Ajoutez cette ligne si ce n'est pas déjà fait
}
