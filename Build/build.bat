pushd %~dp0
@echo "========Updating buildkit started========"

py install-buildkit.py

@echo "========Updating buildkit done========"

@echo "========Updating buildkit started========"

py tools\build.py

@echo "========Updating buildkit done========"
popd
if NOT '%1' == '-g' (pause)
