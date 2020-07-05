#include <iostream>
#include<stdio.h>
#include<stdlib.h>


class node{
public:
	int data;
	node* p_next;
	void AddNode(node a, int b);
};

node *p_head = NULL;

void node :: AddNode(node *ap_next , int ap_data) {
	if (p_head != NULL) {
		node* newNode = (node*)malloc(sizeof(node));
		newNode->data = ap_data; 
		newNode->p_next = ap_next;
	}
	else {
		p_head = (node*)malloc(sizeof(node));
		p_head->p_next = ap_next;
		p_head->data = ap_data;
	}
}

int main() {
	node frist;

	AddNode(frist,10);

	printf("%d", frist.data);

}
