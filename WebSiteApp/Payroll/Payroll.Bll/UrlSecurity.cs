using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Payroll.Bll
{
    public class UrlSecurity
    {
        /*Cette classe a pour but de prévenir les changements dans les QueryStrings par les utilisateurs eux-mêmes
         * dans le but pour d'avoir accès à des ressource auxquels normalement ils n'ont pas 
         * (un employé d'une autre compagnie par exemple dans notre cas).
         * 
         * Une variable est ajoutée au QueryString (Digest) contenant un hash des paramètres à protéger obtenus en appliquant un MD5
         * à ces derniers combinés à une valeur arbitraire (Salt).
         * 
         * Lorsque la page est postée au serveur, avant tout traitement, on vérifie qu'en ré-appliquant le hash 
         * aux paramètres concernés du QueryString on obtient bien la valeur de "Digest".
        */

        private const string urlSalt = "H3#@*ALMLLlk31q411ncL#@..."; // le fameux "salt" (peut être n'importe quel string)!

        public string CreateTamperProofUrl(string url, string nonTamperProofParams, string tamperProofParams)
        {
            //Génère le URL complet avec le paramètre "digest"

            string tempUrl = url;
            if (!String.IsNullOrEmpty(nonTamperProofParams) || !String.IsNullOrEmpty(tamperProofParams))
                url += "?";

            //Ajouter les paramètres du QueryString s'ils existent
            if (!String.IsNullOrEmpty(nonTamperProofParams)) 
                url += nonTamperProofParams;
            if (!String.IsNullOrEmpty(tamperProofParams))
                url += "&" + tamperProofParams;

            //Ajouter le paramètre "Digest" s'il y a lieu
            if (!String.IsNullOrEmpty(tamperProofParams))
                url += String.Concat("&Digest=", GetDigest(tamperProofParams));

            return url;
        }

        public string GetDigest(string tamperProofParams)
        {
            //Calcul la valeur du "Digest"
            string digest = String.Empty;
             
            // Combiner les paramètres concernés avec le "salt"
            string input = String.Concat(urlSalt, tamperProofParams, urlSalt);

            Byte[] hashedDataBytes; // Le tableau de bytes devant contenir la valeur cryptée de "input"
            UTF8Encoding encoder = new UTF8Encoding(); // Pour transformer "input" en un tableau de bytes

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider(); // Pour calculer le hash

            //Calcul du hash en passant la valeur de "input" en bytes
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));

            // Convertir le résultat du hash (bytes) en un "Base-64 String" et enlever les éventuels charactères '='  à la fin 
            // car il pose problème dans le QueryString dans cette position
            digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

            return digest;
        }

        public bool IsUrlNotTampered(string tamperProofParams, string digestReceived)
        {
            //Vérifier que le Url n'a pas été modifié

            //Calculer ce que le "Digest" devrait être
            string expectedDigest = GetDigest(tamperProofParams);

            if (String.IsNullOrEmpty(digestReceived))
            {
                // Pas de Digest donc on renvoi false
                return false;
            }
            else
            {
                // Si des '+' étaient compris dans le Digest du QueryString, ils seront transformés par le WebServer en espaces (chose normale)
                // il faut donc les remettre avant de comparer
                digestReceived = digestReceived.Replace(" ", "+");

                // Comparer les Digest
                if (String.Compare(digestReceived, expectedDigest) != 0)
                    return false;
                else
                    return true;
            }
        }


    }
}
