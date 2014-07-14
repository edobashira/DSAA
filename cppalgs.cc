#include <algorithm>
#include <cstdio>
#include <vector>
#include <iostream>

using namespace std;

template<class T>
void Swap(int i, int j, vector<T>* v) {
  vector<T>& w = *v;
  T t = w[i];
  w[i] = w[j];
  w[j] = t;
}

template<class T>
void Permute(int n, vector<T>* v) {
  vector<T>& w = *v;
  if (n == w.size() - 1) {
    for (int i = 0; i != w.size(); ++i)
      cerr << w[i] << " ";
    cerr << endl;
  } else {
    for (int i = n; i != w.size(); ++i) {
      Swap(n, i, v);
      Permute(n + 1, v);
      Swap(n, i, v);
    }
  }
}

struct E {
  E* next;
  int val;
};

E* Add(E* e, int val) {
  E* f = new E;
  f->val = val;
  f->next = 0;
  e->next = f;
  return f;
}

void PrintList(E* e) {
  for (; e != 0; e = e->next) {
    cout << e->val << "\n";
    //cout << e->val << " ";
  }
}


E* ReverseList(E* e) {
  E* prev = 0;
  for (; e != 0;) {
    E* next = e->next;
    e->next = prev;
    prev = e; 
    e = next;    
  }
  return prev;
}

void Reverse(string *str) {
  string& s = *str;
  for (int i = 0; i < s.size() / 2; ++i) {
    char c = s[i];
    s[i] = s[s.size() - 1 - i];
    s[s.size() - 1 - i] = c;
  }
}

int Combinations(int n, int k) {
  if (n == k) return 1;
  int v[n + 1][k + 1];
  for (int i = 0; i < (n + 1); ++i) {
    for (int j = 0; j <= i; ++j) {
    v[i][j] = i == j ? 1 : (j == 0 ? 1 : v[i - 1][j - 1] + v[i - 1][j]);
    }
  }
 return v[n][k];
}

int binomialCoeff(int n, int k) {
  vector<int> C(k + 1);
  C[0] = 1;
  for (int i = 1; i <= n; i++)
    for (int j = min(i, k); j > 0; j--)
      C[j] = C[j] + C[j-1];
  return C[k];
}


template<class T>
void Print(const vector<T>& v) {
  if (v.size() == 0) {
    cout << "-" << endl;
  } else {
    for (const T& t : v)
      cout << t << ",";
    cout << "\n";
  }
}

template<class T>
void PowerSet(const  vector<T>& v, int n, vector<T>* ps, int* num) {
  Print(*ps);
  (*num)++;
  for (int i = n; i < v.size(); ++i) {
    ;ps->push_back(v[i]);
    PowerSet(v, i + 1, ps, num);
    ps->pop_back();
  }
}

int main() { 
  E head = {0, 10};
  E* tail = &head;
  tail = Add(tail, 20);
  tail = Add(tail, 30);
  tail = Add(tail, 40);
  tail = Add(tail, 50);
  tail = Add(tail, 60);
  //PrintList(&head);
  E* rhead = ReverseList(&head);
  PrintList(rhead);
  string s = "hellop";
  
  Reverse(&s);
  cout << s << endl;
  cout << binomialCoeff(5, 2) << endl;
  
  vector<int> v = {1, 2, 3, 4};
  vector<int> ps;
  int num = 0;
  cout << "\n";
  PowerSet(v, 0, &ps, &num);
  cout << num << "\n";
  return 0;
}

