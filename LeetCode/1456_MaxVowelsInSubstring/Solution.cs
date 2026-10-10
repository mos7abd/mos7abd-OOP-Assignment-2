
public class Solution
{
    // Finds the maximum number of vowels in a substring of length k.
    public int MaxVowels(string s, int k)
    {
        // Stores the number of vowels in the current window.
        int currentVowels = 0;

        // Stores the maximum number of vowels found.
        int maxVowels = 0;

        // Count vowels in the first window of size k.
        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
            {
                currentVowels++;
            }
        }

        // Save the count of the first window.
        maxVowels = currentVowels;

        // Slide the window one character at a time.
        for (int i = k; i < s.Length; i++)
        {
            // Remove the character leaving the window.
            if (IsVowel(s[i - k]))
            {
                currentVowels--;
            }

            // Add the character entering the window.
            if (IsVowel(s[i]))
            {
                currentVowels++;
            }

            // Keep the largest count found so far.
            maxVowels = Math.Max(maxVowels, currentVowels);

            // Stop early if all k characters are vowels.
            if (maxVowels == k)
            {
                return maxVowels;
            }
        }

        // Return the maximum number of vowels found.
        return maxVowels;
    }

    // Checks whether a character is an English vowel.
    private bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i'
            || c == 'o' || c == 'u';
    }
}
