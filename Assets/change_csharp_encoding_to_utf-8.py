import os
import chardet
import codecs

# 변환할 디렉터리 경로
directory_path = "C:/Users/USER/Desktop/Git/Gather_Together/Assets/Scripts"

# 변경할 인코딩 설정
new_encoding = "utf-8"

# 모든 하위 디렉터리의 C# 파일 찾기
for root, dirs, files in os.walk(directory_path):
    for file_name in files:
        if file_name.endswith(".cs"):
            file_path = os.path.join(root, file_name)

            # 현재 파일의 인코딩 감지
            with open(file_path, 'rb') as file:
                result = chardet.detect(file.read())

            # 현재 인코딩 출력
            print(f"Current Encoding of {file_path}: {result['encoding']}")

            # 파일을 새로운 인코딩으로 다시 쓰기
            with codecs.open(file_path, 'r', encoding=result['encoding']) as source_file:
                content = source_file.read()
                with codecs.open(file_path, 'w', encoding=new_encoding) as target_file:
                    target_file.write(content)

            print(f"{file_path}의 인코딩이 {new_encoding}로 변경되었습니다.")
