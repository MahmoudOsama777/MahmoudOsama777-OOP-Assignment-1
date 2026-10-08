public class Solution {
    public int MaxOperations(int[] nums, int k) {
        int numberOfOperations = 0;

        Array.Sort(nums);

        int right = nums.Length  - 1;
        int left = 0;

        while (left < right)
        {
            if (nums[left] + nums[right] == k)
            {
                numberOfOperations++;   
                right--;
                left++;
            }

            if(nums[left] + nums[right] < k)
                left++;

            if(nums[left] + nums[right] > k)
                right--;
        }

        return numberOfOperations;

    }
}