#pragma once

// ���۰� ���ſ��� ���������� �۾��� ������ ������ Ŭ����
class TW_ExchangeManager
{
protected:
	// ���� �Ǵ� ������ ���� �Ҵ�� �޸��� ��ü ũ��� ���� �۾����� ũ��
	int m_total_size, m_cur_size;
	// ���� �Ǵ� ������ ���ؼ� �Ҵ�� �޸��� ���� �ּ�
	char* mp_data;

public:
	TW_ExchangeManager();  // ��ü ������
	~TW_ExchangeManager(); // ��ü �ı���

	// ���� �Ǵ� ���ſ� ����� �޸𸮸� �Ҵ��Ѵ�.
	// a_data_size�� �Ҵ��� ũ�⸦ ����ϸ� �� �Լ��� ��ȯ ������
	// �Ҵ�� �޸��� �ּҸ� ��ȯ�Ѵ�.(��ȯ�� �ּҴ� �� Ŭ������ �����ϰ� �ִ�)
	char* MemoryAlloc(int a_data_size);
	// ���� �Ǵ� ���ſ� ���Ǵ� �޸𸮸� �����Ѵ�.
	void DeleteData();

	inline int GetTotalSize() { return m_total_size; } // �Ҵ�� �޸��� ũ�⸦ ��ȯ�Ѵ�.
	inline int GetCurSize() { return m_cur_size; }     // ���� �۾����� �޸��� ��ġ�� ��ȯ�Ѵ�.
};


class TW_SendManager : public TW_ExchangeManager
{
public:
	// ���� ������ ��ġ�� ũ�⸦ ����Ѵ�.
	int GetPosition(char** ap_data, int a_data_size = 2048);
	// ������ �Ϸ�Ǿ����� üũ�Ѵ�.
	inline char IsProcessing() { return m_total_size != m_cur_size; }
};

class TW_RecvManager : public TW_ExchangeManager
{
public:
	// ���ŵ� �����͸� ���� ���ŵ� �����Ϳ� �߰��Ѵ�.

	int AddData(char* ap_data, int a_size);
	// ���ŵ� �����͸� �ϳ��� ��ģ �޸��� ���� �ּҸ� ��´�.
	inline char* GetData() { return mp_data; }
};