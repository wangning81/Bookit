using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Biz
{
    public class IndexMinPQ<TObjKey, TCmpVal>
        where TCmpVal : IComparable<TCmpVal>
    {
        private TCmpVal[] pq;
        private TObjKey[] keys;
        private int count = 0;
        private int capicity = 16;
        private IDictionary<TObjKey, int> indices = new Dictionary<TObjKey, int>();
        
        public IndexMinPQ()
        {
            pq = new TCmpVal[capicity + 1];
            keys = new TObjKey[capicity + 1];
        }

        public void Enqueue(TObjKey obj, TCmpVal val)
        {
            ++count;
            pq[count] = val;
            keys[count] = obj;
            indices[obj] = count;

            Swim(count);

            if (count == capicity - 1)
                Resize(capicity * 2);
        }
        public TObjKey Dequeue()
        {
            TObjKey ret = keys[1];
            indices.Remove(ret);

            count--;

            if (count > 0)
            {
                keys[1] = keys[count + 1];
                indices[keys[1]] = 1;
                pq[1] = pq[count + 1];

                Sink(1);
            }

            if (count < capicity / 4)
                Resize(capicity / 2);

            return ret;
        }

        public void Change(TObjKey key, TCmpVal val)
        {
            int i = indices[key];
            pq[i] = val;

            Swim(i);
            Sink(i);
        }

        public void Clear()
        {
            capicity = 16;
            pq = new TCmpVal[capicity];
            keys = new TObjKey[capicity];
            indices.Clear();
            count = 0;
        }

        private void Resize(int newSize)
        {
            capicity = newSize;
            TCmpVal[] newPq = new TCmpVal[capicity + 1];
            TObjKey[] newKeys = new TObjKey[capicity + 1];

            for (int i = 1; i <= count; i++)
            { 
                newPq[i] = pq[i];
                newKeys[i] = keys[i];
            }

            pq = newPq;
            keys = newKeys;
        }

        private void Swim(int k)
        {
            TCmpVal val = pq[k];
            TObjKey key = keys[k];

            while (k > 1 && pq[k].CompareTo(pq[k / 2]) < 0)
            {
                pq[k] = pq[k / 2];
                keys[k] = keys[k / 2];
                indices[keys[k / 2]] = k;
                k /= 2;
            }

            pq[k] = val;
            keys[k] = key;
            indices[key] = k;
        }

        private void Sink(int k)
        {
            TCmpVal val = pq[k];
            TObjKey key = keys[k];

            while (k <= count / 2)
            {
                int j = k * 2;
                if (j < count && pq[j].CompareTo(pq[j + 1]) > 0)
                    j += 1;

                if (pq[j].CompareTo(pq[k]) < 0)
                {
                    pq[k] = pq[j];
                    keys[k] = keys[j];
                    indices[keys[j]] = k;
                    k = j;
                }
                else
                    break;
            }

            pq[k] = val;
            keys[k] = key;
            indices[key] = k;
        }

        public bool IsEmpty 
        {
            get
            {
                return count == 0;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public TCmpVal this[TObjKey key]
        {
            get
            {
                if (indices.ContainsKey(key))
                    return pq[indices[key]];
                return default(TCmpVal);
            }
            set
            {
                if (indices.ContainsKey(key))
                    Change(key, value);
                else
                    Enqueue(key, value);
            }
        }
    }
}
